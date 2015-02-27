using UnityEngine;
using System.Collections;

public class InAttackRange : MonoBehaviour {

	public bool colliding = false;
	//public Collider colWith;
	public GameObject sphere;
	public float rayDist = 2f;
	public float sphereRate = .5f;

	public LayerMask enemies;

	void Start() {
		//StartCoroutine (rayCheck());
	}

	public RaycastHit rayCheck() { //IF PLAYER ISNT ATTACKING CHECK THIS FUNCTION?
		Debug.Log ("in raycheck");
		RaycastHit hit;
		if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayDist, enemies)) {
			if(hit.transform.gameObject.name != transform.parent.transform.parent.gameObject.name) { //make sure you didnt hit yourself.
				Debug.Log("hit a player in raycheck");
				colliding = true;
				//colWith = hit.collider;
				Instantiate(sphere, hit.point, Quaternion.identity);
			}
		} else {
			colliding = false;
			//colWith = null;
		}
		return hit;
	}
	

}
