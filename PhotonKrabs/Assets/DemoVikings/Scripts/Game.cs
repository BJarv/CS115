using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	[HideInInspector]
	public Transform[] spawnPoints;
	
	
	// Use this for initialization
	void Start () {
		GameObject[] spawns = GameObject.FindGameObjectsWithTag("Respawn");
		spawnPoints = new Transform[spawns.Length];
		for(int i = 0; i < spawns.Length; i++) {
			spawnPoints[i] = spawns[i].transform;
		}
	}


	public Vector3 getSpawn() {
		return spawnPoints[Random.Range (0, spawnPoints.Length)].position;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
