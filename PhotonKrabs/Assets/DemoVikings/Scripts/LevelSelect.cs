using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {

	public GameObject levels;
	public GameObject titleButton;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadLevel(string levelName){
		Application.LoadLevel (levelName);
	}

	public void swapToLevels() {
		titleButton.SetActive (false);
		levels.SetActive (true);

	}


}
