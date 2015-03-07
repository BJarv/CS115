using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	GameManagerVik gameMan;
	ChatVik chat;
	void Start() {
		gameMan = GameObject.Find ("Code").GetComponent<GameManagerVik>();
		chat = GameObject.Find ("Code").GetComponent<ChatVik>();
	}

	public float HP = 1f;

	[RPC]
	public void TakeDamage(float amt, string killerName) {
		//if health is more than 1, do something here to decide when to die
		Die(killerName);
	}

	void Die(string killerName) {
		if(GetComponent<PhotonView>().isMine){
			Debug.Log ("in die");
			chat.SendChat(killerName + " fragged " + PhotonNetwork.playerName + "!");
			PhotonNetwork.Destroy(gameObject);
			gameMan.mainCamObj.SetActive (true);
			gameMan.RespawnPlayer();
		}
	}

}
