using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game : MonoBehaviour {

	[HideInInspector]
	public Transform[] spawnPoints;
	public int winKill = 10;
	public float gameOverDelay = 5f;
	public GameObject winScreen;
	
	
	// Use this for initialization
	void Start () {
		winScreen = transform.Find ("Canvas/Panel").gameObject;
		GameObject[] spawns = GameObject.FindGameObjectsWithTag("Respawn");
		spawnPoints = new Transform[spawns.Length];
		for(int i = 0; i < spawns.Length; i++) {
			spawnPoints[i] = spawns[i].transform;
		}
	}


	public Vector3 getSpawn() {
		Transform newspawn = spawnPoints[Random.Range (0, spawnPoints.Length)];
		Debug.Log (newspawn.name);
		return newspawn.position;
	}

	// Update is called once per frame
	void Update () {

	}

	[RPC]
	public void gameover(string winner) {
		//string winnerName = winner[0].ToString ();
		winScreen.SetActive(true);
		winScreen.transform.Find ("Text").GetComponent<Text>().text = winner + "Wins!";
		StartCoroutine("quit");
	}

	IEnumerator quit() {
		yield return new WaitForSeconds(gameOverDelay);
		PhotonNetwork.LeaveRoom();
	}
	
}
