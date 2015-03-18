using UnityEngine;
using System.Collections;
 
public class ColorSwitch : MonoBehaviour {

	public Material[] colors;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (PhotonNetwork.player.customProperties["c"]);
		//Debug.Log ((int)GetComponent<PhotonView> ().owner.customProperties("c"));
		GetComponent<SkinnedMeshRenderer> ().material = colors [(int)GetComponent<PhotonView> ().owner.customProperties["c"]];

	}
	
	public void colorSwap(int colorIndex) {
		GetComponent<PhotonView> ().RPC ("sendColorSwap", PhotonTargets.All, colorIndex);
	}

	[RPC]
	public void sendColorSwap(int colorIndex) {
		//if(PhotonNetwork.isMasterClient){
			GetComponent<SkinnedMeshRenderer> ().material = colors [colorIndex];
		//}
	}

	void OnGUI() {

		GUILayout.BeginHorizontal();
		GUILayout.Label("CHOOSE COLOR:", GUILayout.Width(150));
		GUI.color = Color.blue;
		if (GUILayout.Button(""))
		{
			PlayerPrefs.SetInt("color", 0);
			colorSwap (PlayerPrefs.GetInt ("color"));
		}
		GUI.color = Color.green;
		if (GUILayout.Button(""))
		{
			
			PlayerPrefs.SetInt("color", 1);
			colorSwap (PlayerPrefs.GetInt ("color"));
		}
		//GUILayout.EndHorizontal();
		
		//GUILayout.BeginHorizontal();
		
		//orange
		GUI.color = new Color(1f, 0.50f, 0.016f, 1f);
		if (GUILayout.Button(""))
		{
			PlayerPrefs.SetInt("color", 2);
			colorSwap (PlayerPrefs.GetInt ("color"));
		}
		GUI.color = Color.magenta;
		if (GUILayout.Button(""))
		{
			PlayerPrefs.SetInt("color", 3);
			colorSwap (PlayerPrefs.GetInt ("color"));
		}
		GUI.color = Color.red;
		if (GUILayout.Button(""))
		{
			PlayerPrefs.SetInt("color", 4);
			colorSwap (PlayerPrefs.GetInt ("color"));
		}
		GUI.color = Color.yellow;
		if (GUILayout.Button(""))
		{
			
			PlayerPrefs.SetInt("color", 5);
			colorSwap (PlayerPrefs.GetInt ("color"));
		}
		GUILayout.EndHorizontal();
	}

}
