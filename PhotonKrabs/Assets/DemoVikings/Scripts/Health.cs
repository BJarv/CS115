using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	GameManagerVik gameMan;
	ChatVik chat;
	ThirdPersonControllerNET controller;
	public float respawnTimer = 5f;

	void Start() {
		gameMan = GameObject.Find ("Code").GetComponent<GameManagerVik>();
		chat = GameObject.Find ("Code").GetComponent<ChatVik>();
		controller = GetComponent<ThirdPersonControllerNET>();
	}

	public float HP = 1f;

	[RPC]
	public void TakeDamage(float amt, string killerName) {
		//if health is more than 1, do something here to decide when to die
		Die(killerName);
		GetComponent<ThirdPersonNetworkVik>().deaths++;
	}

	void Die(string killerName) {
		if(GetComponent<PhotonView>().isMine){
			Debug.Log ("in die");
			chat.SendChat(killerName + " fragged " + PhotonNetwork.playerName + "!");
			//PhotonNetwork.Destroy(gameObject);
			controller.isDead = true;
			controller.cam.SetActive(false);
			transform.position = new Vector3(1000f, 1000f, 1000f);
			gameMan.mainCamObj.SetActive (true);
			StartCoroutine("respawn");
			//gameMan.RespawnPlayer();
		}
	}

	IEnumerator respawn() {
		yield return new WaitForSeconds(respawnTimer);
		controller.isDead = false;
		controller.cam.SetActive(true);
		transform.position = gameMan.game.getSpawn();
		gameMan.mainCamObj.SetActive (false);
	}

}
