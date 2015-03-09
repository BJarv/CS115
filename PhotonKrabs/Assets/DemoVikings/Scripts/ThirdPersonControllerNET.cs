using UnityEngine;
using System.Collections;

public delegate void JumpDelegate ();

public class ThirdPersonControllerNET : MonoBehaviour
{
	bool fire1OnCD = false;
	bool fire2OnCD = false;
	public float fire1CD = 1f;
	public float fire2CD = 2f;
	public float dashTime = .7f;
	bool killingOther = false;
	public bool dashing = false;
	public GameObject cam;
	public bool isDead = false;
	public float parryStrength = 500f;


	public InAttackRange range;
	public Vector3 dashPower;

	public Rigidbody target;
		// The object we're steering
	public float speed = 4.0f, walkSpeedDownscale = 1.0f, turnSpeed = 0.0f, mouseTurnSpeed = 0.3f, jumpSpeed = 1.0f;
		// Tweak to ajust character responsiveness
	public LayerMask groundLayers = -1;
		// Which layers should be walkable?
		// NOTICE: Make sure that the target collider is not in any of these layers!
	public float groundedCheckOffset = 0.7f;
		// Tweak so check starts from just within target footing
	public bool
		showGizmos = true,
			// Turn this off to reduce gizmo clutter if needed
		requireLock = true,
			// Turn this off if the camera should be controllable even without cursor lock
		controlLock = false;
			// Turn this on if you want mouse lock controlled by this script
	public JumpDelegate onJump = null;
		// Assign to this delegate to respond to the controller jumping
	
	
	private const float inputThreshold = 0.01f,
		groundDrag = 5.0f,
		directionalJumpFactor = 0.7f;
		// Tweak these to adjust behaviour relative to speed
	private const float groundedDistance = 0.5f;
		// Tweak if character lands too soon or gets stuck "in air" often
		
	
	private bool grounded, walking;

	private bool isRemotePlayer = true;

	public bool Grounded
	// Make our grounded status available for other components
	{
		get
		{
			return grounded;
		}
	}

    public void SetIsRemotePlayer(bool val)
    {
        isRemotePlayer = val;
    }

	void Reset ()
	// Run setup on component attach, so it is visually more clear which references are used
	{
		Setup ();
	}
	
	
	void Setup ()
	// If target is not set, try using fallbacks
	{
		if (target == null)
		{
			target = GetComponent<Rigidbody> ();
		}
	}
	
		
	void Start ()
	// Verify setup, configure rigidbody
	{
		Setup ();
			// Retry setup if references were cleared post-add
		
		if (target == null)
		{
			Debug.LogError ("No target assigned. Please correct and restart.");
			enabled = false;
			return;
		}
		range = transform.Find ("Camera/AttackRange").GetComponent<InAttackRange>();
		cam = transform.Find("Camera").gameObject;
		target.freezeRotation = true;
			// We will be controlling the rotation of the target, so we tell the physics system to leave it be
		walking = false;
	}
	
	
	void Update ()
	// Handle rotation here to ensure smooth application.
	{
        if (isRemotePlayer) return;
		if(!isDead){
			if(attackable ()) {
				if(Input.GetMouseButtonDown(0)) {
					fire1OnCD = true;
					StartCoroutine("fire1OffCD");
					Fire1 ();
				} else if (Input.GetMouseButtonDown (1)) {
					fire2OnCD = true;
					StartCoroutine ("fire2OffCD");
					Fire2();
				}
			}
			if(dashing && !killingOther) {
				RaycastHit hit = range.sphereCheck();
				if(range.colliding && hit.collider.tag == "Player") {
					if(hit.collider.GetComponent<ThirdPersonControllerNET>().dashing) { //if the other player is also dashing, parry. may need a more 'networky' solution
						parryRecoilSelf(hit);
					} else { //otherwise kill player
						killingOther = true;
						GetComponent<PhotonView>().RPC ("incKill", PhotonTargets.AllBuffered);//kills++;
						hit.transform.gameObject.GetComponent<PhotonView>().RPC ("TakeDamage", PhotonTargets.AllBuffered, 1f, PhotonNetwork.playerName);
					}
				}
			}
		}


	}
	
	
	float SidestepAxisInput
	// If the right mouse button is held, the horizontal axis also turns into sidestep handling
	{
		get
		{
			//if (Input.GetMouseButton (1))
			//{
			//	float sidestep = -(Input.GetKey(KeyCode.Q)?1:0) + (Input.GetKey(KeyCode.E)?1:0);
            //    float horizontal = Input.GetAxis ("Horizontal");
			//	
			//	return Mathf.Abs (sidestep) > Mathf.Abs (horizontal) ? sidestep : horizontal;
			//}
			//else
			//{
            //float sidestep = -(Input.GetKey(KeyCode.Q) ? 1 : 0) + (Input.GetKey(KeyCode.E) ? 1 : 0);
			float sidestep = Input.GetAxis("Horizontal");
            return sidestep;
			//}
		}
	}
	
	
	void FixedUpdate ()
	// Handle movement here since physics will only be calculated in fixed frames anyway
	{
      
		grounded = (Physics.Raycast (
			target.transform.position + target.transform.up * -groundedCheckOffset,
			target.transform.up * -1,
			groundedDistance,
			groundLayers
		) && !dashing);
		//Debug.Log (grounded);
			// Shoot a ray downward to see if we're touching the ground

        if (isRemotePlayer) return;


		if (grounded && !isDead)
		{
			target.drag = groundDrag;
				// Apply drag when we're grounded
			
			if (Input.GetButton ("Jump"))
			// Handle jumping
			{
				target.AddForce (
					jumpSpeed * target.transform.up +
						target.velocity.normalized * directionalJumpFactor,
					ForceMode.VelocityChange
				);
					// When jumping, we set the velocity upward with our jump speed
					// plus some application of directional movement
				
				if (onJump != null)
				{
					onJump ();
				}
			}
			else
			// Only allow movement controls if we did not just jump
			{
				Vector3 movement = Input.GetAxis ("Vertical") * target.transform.forward +
					SidestepAxisInput * target.transform.right;
				
				float appliedSpeed = walking ? speed / walkSpeedDownscale : speed;
					// Scale down applied speed if in walk mode
				
				//if (Input.GetAxis ("Vertical") < 0.0f)
				// Scale down applied speed if walking backwards
				//{
				//	appliedSpeed /= walkSpeedDownscale;
				//}

				if (movement.magnitude > inputThreshold)
				// Only apply movement if we have sufficient input
				{
					target.AddForce (movement.normalized * appliedSpeed, ForceMode.VelocityChange);
				}
				else
				// If we are grounded and don't have significant input, just stop horizontal movement
				{
					target.velocity = new Vector3 (0.0f, target.velocity.y, 0.0f);
					return;
				}
			}
		}
		else
		{
			target.drag = 0.0f;
				// If we're airborne, we should have no drag
		}
	}
	
	
	void OnDrawGizmos ()
	// Use gizmos to gain information about the state of your setup
	{
		if (!showGizmos || target == null)
		{
			return;
		}
		
		Gizmos.color = grounded ? Color.blue : Color.red;
		Gizmos.DrawLine (target.transform.position + target.transform.up * -groundedCheckOffset,
			target.transform.position + target.transform.up * -(groundedCheckOffset + groundedDistance));
	}



	public bool attackable() {
		if (fire1OnCD || fire2OnCD) { //add any cooldowns or other restrictions to attack here
			return false;
		} else {
			return true;
		}
	}

	IEnumerator fire1OffCD() {
		yield return new WaitForSeconds(fire1CD);
		fire1OnCD = false;
	}

	IEnumerator fire2OffCD() {
		yield return new WaitForSeconds(fire2CD);
		fire2OnCD = false;
	}

	IEnumerator dashingOff() {
		yield return new WaitForSeconds(dashTime);
		dashing = false;
		killingOther = false;
	}


	void Fire1() {
		RaycastHit hit = range.sphereCheck();
		//Debug.Log ("in fire1");
		if(range.colliding){ //something is in range
			//Debug.Log ("In colliding");
			
			if(hit.collider.tag == "wall") { //object is wall
				//play wall hit noise
				//Debug.Log ("In wall");
				
			} else if(hit.collider.tag == "Player") { //if object hit is enemy
				//play player hit noise
				GetComponent<PhotonView>().RPC ("incKill", PhotonTargets.AllBuffered);//kills++;

				hit.transform.gameObject.GetComponent<PhotonView>().RPC ("TakeDamage", PhotonTargets.AllBuffered, 1f, PhotonNetwork.playerName);
			}
			
		} else {
			//Debug.Log ("In wiff");
			//play wiff noise
		}
	}

	void Fire2(){
		dashing = true;
		StartCoroutine("dashingOff");
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		Vector3 dashForce = cam.transform.TransformDirection(Vector3.forward) * 1500f;
		//Debug.Log (dashForce);
		GetComponent<Rigidbody>().AddForce (dashForce);

	}

	public void parryRecoilSelf(RaycastHit hit){
		Vector3 parryForce = Vector3.Reflect(hit.point - transform.position, hit.normal); //get a vector that is perpendicular to the point we just hit
		hit.collider.GetComponent<PhotonView>().RPC("parryRecoilOther", hit.collider.GetComponent<PhotonView>().owner, parryForce); //tell the other player to recoil in the opposite direction
		GetComponent<Rigidbody>().AddForce(parryForce * parryStrength, ForceMode.Acceleration);
	}

	[RPC]
	public void parryRecoilOther(Vector3 parryForce) { //NEEDS TESTING
		GetComponent<Rigidbody>().AddForce (parryForce * parryStrength, ForceMode.Acceleration);
	}

	[RPC]
	void incKill() { //increases kill count by 1, for deaths this is done in health.cs under takedamage
		GetComponent<ThirdPersonNetworkVik>().kills++;
	}

}
