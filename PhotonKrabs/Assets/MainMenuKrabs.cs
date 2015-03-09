using UnityEngine;
using System.Collections;

public class MainMenuKrabs : MonoBehaviour
{
	public GameObject menu;
<<<<<<< HEAD
	public Texture[] textures;
	public float changeInterval = 0.33F;
	public Renderer rend;
=======
	public Material [] krab_colors;
>>>>>>> 1ec67c2f7f075531887fe943b1732eba685b8586


    void Awake()
    {
		rend = GetComponent<Renderer>();
        //PhotonNetwork.logLevel = NetworkLogLevel.Full;

        //Connect to the main photon server. This is the only IP and port we ever need to set(!)
        if (!PhotonNetwork.connected)
            PhotonNetwork.ConnectUsingSettings("v1.0"); // version of the game/demo. used to separate older clients from newer ones (e.g. if incompatible)

        //Load name from PlayerPrefs
        PhotonNetwork.playerName = PlayerPrefs.GetString("playerName", "Guest" + Random.Range(1, 9999));

        //Set camera clipping for nicer "main menu" background
        Camera.main.farClipPlane = Camera.main.nearClipPlane + 0.1f;

    }

    private string roomName = "myRoom";
	private string PlayerName = "Guest";
    private Vector2 scrollPos = Vector2.zero;
	public void changePlayerName(string nameChange){
		PlayerName = nameChange;
		PhotonNetwork.playerName = PlayerName;
		Debug.Log (PlayerName);
	}
	public void joinRoomName(string joinRoom){
		roomName = joinRoom;
	}
	public void joinRoom(int join){
		if (join == 1){
			PhotonNetwork.JoinRoom(roomName);

		}
	}

	public void createRoomName(string createRoom){
		roomName = createRoom;
		//Debug.Log (roomName);
	}

	public void changeColor (int color) {
		GameObject.Find ("krab_new_animations/Cube_006").GetComponent<SkinnedMeshRenderer> ().materials[0] = krab_colors[0];
	}

	public void createRoom(int create){
		if (create == 1){
		PhotonNetwork.CreateRoom(roomName, new RoomOptions() { maxPlayers = 10 }, TypedLobby.Default);
		//Debug.Log (PhotonNetwork.playerName);
			//menu.SetActive(false);
		}

	}
	public void changeColor(int color){

	}
	void Update()
    {
        if (!PhotonNetwork.connected)
        {
            menu.SetActive(false);   //Wait for a connection
        }


        if (PhotonNetwork.room != null)
			menu.SetActive(false); //Only when we're not in a Room
		else
			menu.SetActive(true);

    }
}
