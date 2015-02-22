using UnityEngine;
using System.Collections;

public class GameManagerVik : Photon.MonoBehaviour {

    // this is a object name (must be in any Resources folder) of the prefab to spawn as player avatar.
    // read the documentation for info how to spawn dynamically loaded game objects at runtime (not using Resources folders)
    public string playerPrefabName = "Charprefab";

	Game game;
	public float respawnTimer = 3f;
	public GameObject mainCamObj;
	public Camera mainCam;

	void Start() {
		game = GetComponent<Game>();
	}
    void OnJoinedRoom()
    {

        StartGame();
    }
    
    IEnumerator OnLeftRoom()
    {
        //Easy way to reset the level: Otherwise we'd manually reset the camera

        //Wait untill Photon is properly disconnected (empty room, and connected back to main server)
        while(PhotonNetwork.room!=null || PhotonNetwork.connected==false)
            yield return 0;

        Application.LoadLevel(Application.loadedLevel);

    }

    void StartGame()
    {
        mainCam.farClipPlane = 1000; //Main menu set this to 0.4 for a nicer BG    
		/*
        //prepare instantiation data for the viking: Randomly diable the axe and/or shield
        bool[] enabledRenderers = new bool[2];
        enabledRenderers[0] = Random.Range(0,2)==0;//Axe
        enabledRenderers[1] = Random.Range(0, 2) == 0; ;//Shield
        
        object[] objs = new object[1]; // Put our bool data in an object array, to send
        objs[0] = enabledRenderers;

        // Spawn our local player
        PhotonNetwork.Instantiate(this.playerPrefabName, game.getSpawn(), Quaternion.identity, 0, objs);
        */
		SpawnPlayer ();
    }

	public void SpawnPlayer() {
		//prepare instantiation data for the viking: Randomly diable the axe and/or shield
		bool[] enabledRenderers = new bool[2];
		enabledRenderers[0] = Random.Range(0,2)==0;//Axe
		enabledRenderers[1] = Random.Range(0, 2) == 0; ;//Shield
		
		object[] objs = new object[1]; // Put our bool data in an object array, to send
		objs[0] = enabledRenderers;
		//
		//Everything above this line in this function isnt needed, just put in for simplicity, definitely change later


		// Spawn our local player
		PhotonNetwork.Instantiate(this.playerPrefabName, game.getSpawn(), Quaternion.identity, 0, objs);
	}

	public void RespawnPlayer() { //callable from other scripts to start respawn coroutine
		StartCoroutine("respawn");
	}

	IEnumerator respawn() { //helper to respawnplayer
		yield return new WaitForSeconds(respawnTimer);
		SpawnPlayer();
	}

	void OnGUI()
	{
		if (PhotonNetwork.room == null) return; //Only display this GUI when inside a room

        if (GUILayout.Button("Leave Room"))
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    void OnDisconnectedFromPhoton()
    {
        Debug.LogWarning("OnDisconnectedFromPhoton");
    }    
}
