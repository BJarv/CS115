using UnityEngine;
using System.Collections;

public class MainMenuVik : MonoBehaviour
{
	public GameObject menu;

	public GUIStyle krab_title;

    void Awake()
    {
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
    private Vector2 scrollPos = Vector2.zero;


	//void Update()
    void OnGUI()
    {
        if (!PhotonNetwork.connected)
        {
            ShowConnectingGUI();
			return;
            //menu.SetActive(false);   //Wait for a connection
        }


        if (PhotonNetwork.room != null)
			return;
			//menu.SetActive(false); //Only when we're not in a Room
		//else
			//menu.SetActive(true);


		GUI.color = Color.white;
		GUI.Label (new Rect(Screen.width * 0.15f , Screen.height * 0.1f , Screen.width * 0.8f , Screen.height * 0.5f), "Krab Klashers", krab_title);

        GUILayout.BeginArea(new Rect((Screen.width - 400) / 2, (Screen.height - 200) / 2, 400, 300));


        //Player name
        GUILayout.BeginHorizontal();
        GUILayout.Label("Player name:", GUILayout.Width(150));
        PhotonNetwork.playerName = GUILayout.TextField(PhotonNetwork.playerName);
        if (GUI.changed)//Save name
            PlayerPrefs.SetString("playerName", PhotonNetwork.playerName);
        GUILayout.EndHorizontal();

        GUILayout.Space(15);


        //Join room by title
        GUILayout.BeginHorizontal();
        GUILayout.Label("JOIN ROOM:", GUILayout.Width(150));
        roomName = GUILayout.TextField(roomName);
        if (GUILayout.Button("GO"))
        {
            PhotonNetwork.JoinRoom(roomName);
        }
        GUILayout.EndHorizontal();

        //Create a room (fails if exist!)
        GUILayout.BeginHorizontal();
        GUILayout.Label("CREATE ROOM:", GUILayout.Width(150));
        roomName = GUILayout.TextField(roomName);
        if (GUILayout.Button("GO"))
        {
            // using null as TypedLobby parameter will also use the default lobby
            PhotonNetwork.CreateRoom(roomName, new RoomOptions() { maxPlayers = 10 }, TypedLobby.Default);
        }
        GUILayout.EndHorizontal();

        //Join random room
        GUILayout.BeginHorizontal();
        GUILayout.Label("JOIN RANDOM ROOM:", GUILayout.Width(150));
        if (PhotonNetwork.GetRoomList().Length == 0)
        {
            GUILayout.Label("..no games available...");
        }
        else
        {
            if (GUILayout.Button("GO"))
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }
        GUILayout.EndHorizontal();

		//choose a color
		GUILayout.BeginHorizontal();
		GUILayout.Label("CHOOSE COLOR:", GUILayout.Width(150));
		GUI.color = Color.blue;
		if (GUILayout.Button(""))
		{

			PlayerPrefs.SetInt("color", 0);
		}
		GUI.color = Color.green;
		if (GUILayout.Button(""))
		{

			PlayerPrefs.SetInt("color", 1);
		}
		//GUILayout.EndHorizontal();

		//GUILayout.BeginHorizontal();

		//orange
		GUI.color = new Color(1f, 0.50f, 0.016f, 1f);
		if (GUILayout.Button(""))
		{
			PlayerPrefs.SetInt("color", 2);
		}
		GUI.color = Color.magenta;
		if (GUILayout.Button(""))
		{
			PlayerPrefs.SetInt("color", 3);
		}
		GUI.color = Color.red;
		if (GUILayout.Button(""))
		{
			PlayerPrefs.SetInt("color", 4);
		}
		GUI.color = Color.yellow;
		if (GUILayout.Button(""))
		{

			PlayerPrefs.SetInt("color", 5);
		}
		GUILayout.EndHorizontal();



		GUI.color = Color.white;
        GUILayout.Space(30);
        GUILayout.Label("ROOM LISTING:");
        if (PhotonNetwork.GetRoomList().Length == 0)
        {
            GUILayout.Label("..no games available..");
        }
        else
        {
            //Room listing: simply call GetRoomList: no need to fetch/poll whatever!
            scrollPos = GUILayout.BeginScrollView(scrollPos);
            foreach (RoomInfo game in PhotonNetwork.GetRoomList())
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(game.name + " " + game.playerCount + "/" + game.maxPlayers);
                if (GUILayout.Button("JOIN"))
                {
                    PhotonNetwork.JoinRoom(game.name);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
        }
	
        GUILayout.EndArea();
    }


    void ShowConnectingGUI()
    {
        GUILayout.BeginArea(new Rect((Screen.width - 400) / 2, (Screen.height - 300) / 2, 400, 300));

        GUILayout.Label("Connecting to Photon server.");
        //GUILayout.Label("Hint: This demo uses a settings file and logs the server address to the console.");

        GUILayout.EndArea();
    }
}
