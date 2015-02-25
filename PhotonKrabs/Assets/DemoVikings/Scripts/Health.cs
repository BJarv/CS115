using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	GameManagerVik gameMan;
	void Start() {
		gameMan = GameObject.Find ("Code").GetComponent<GameManagerVik>();
	}

	public float HP = 1f;

	[RPC]
	public void TakeDamage(float amt) {
		//if health is more than 1, do something here to decide when to die
		Die();
	}

	void Die() {
		if(GetComponent<PhotonView>().isMine){
			Debug.Log ("in die");
			PhotonNetwork.Destroy(gameObject);
			gameMan.mainCamObj.SetActive (true);
			gameMan.RespawnPlayer();
		}
	}

}
