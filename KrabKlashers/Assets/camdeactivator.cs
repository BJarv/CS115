using UnityEngine;
using System.Collections;

public class camdeactivator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// deactivate the CharacterController that is not Mine in the Network 
		if (!networkView.isMine) {
			GetComponent<CharacterController>().enabled = false;
			GetComponentInChildren<AudioListener>().enabled = false;
			GetComponentInChildren<Camera>().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
