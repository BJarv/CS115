using UnityEngine;
using System.Collections;
 
public class ColorSwitch : MonoBehaviour {

	public Material[] colors;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void colorSwap(int colorIndex) {
		//GetComponent<SkinnedMeshRenderer> ().Material [0] = colors [colorIndex];
		GetComponent<PhotonView> ().RPC ("sendColorSwap", PhotonTargets.All, colorIndex);
	}

	[RPC]
	public void sendColorSwap(int colorIndex) {
		GetComponent<SkinnedMeshRenderer> ().material = colors [colorIndex];
	}

}
