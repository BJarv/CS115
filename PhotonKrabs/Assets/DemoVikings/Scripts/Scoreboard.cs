using UnityEngine;
using System.Collections;

public class Scoreboard : MonoBehaviour {
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(PhotonNetwork.playerName); //own player name
		//GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		//for(int i = 0; i < players.Length; i++) {
			//Debug.Log ("view name: " + players[i].GetComponent<PhotonView>().owner.name); //other players names
		//}
	}

	void OnGUI() {
		if(Input.GetKey (KeyCode.LeftShift)) {
			GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, 50, 400, 500));

			GUILayout.BeginHorizontal("Box"); 
			//name
			GUILayout.BeginVertical(GUILayout.Width(150));
			GUILayout.Label ("Player", GUILayout.Width(75));
			GUILayout.EndVertical();
			
			//kills
			GUILayout.BeginVertical(GUILayout.Width(150));
			GUILayout.Label ("Kills", GUILayout.Width(75));
			GUILayout.EndVertical();
			
			//deaths
			GUILayout.BeginVertical(GUILayout.Width(150));
			GUILayout.Label ("Deaths", GUILayout.Width(75));
			GUILayout.EndVertical();
			
			GUILayout.EndHorizontal();

			foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
				scoreLine (player);
			}
			GUILayout.EndArea();
		}
	}

	void scoreLine(GameObject player) {
		GUILayout.BeginHorizontal("Box"); 
		//name
		GUILayout.BeginVertical(GUILayout.Width(150));
		GUILayout.Label (player.GetComponent<PhotonView>().owner.name, GUILayout.Width(75));
		GUILayout.EndVertical();

		//kills
		GUILayout.BeginVertical(GUILayout.Width(150));
		GUILayout.Label (player.GetComponent<ThirdPersonNetworkVik>().kills.ToString(), GUILayout.Width(75));
		GUILayout.EndVertical();

		//deaths
		GUILayout.BeginVertical(GUILayout.Width(150));
		GUILayout.Label (player.GetComponent<ThirdPersonNetworkVik>().deaths.ToString(), GUILayout.Width(75));
		GUILayout.EndVertical();

		GUILayout.EndHorizontal();
	}


}
