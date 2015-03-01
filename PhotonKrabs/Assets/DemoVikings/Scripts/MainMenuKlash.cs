using UnityEngine;
using System.Collections;

public class MainMenuKlash : MonoBehaviour
{
	public GameObject menu;

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

	void Update(){
        if (!PhotonNetwork.connected){
            menu.SetActive(false);   //Wait for a connection
        }
        if (PhotonNetwork.room != null)
			menu.SetActive(false); //Only when we're not in a Room
		else
			menu.SetActive(true);
    }
}
