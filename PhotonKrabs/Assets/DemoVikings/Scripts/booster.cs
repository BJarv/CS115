﻿using UnityEngine;
using System.Collections;

public class booster : MonoBehaviour {

	public float boost;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player") {
			col.GetComponent<Rigidbody>().velocity = Vector3.zero;
			Vector3 boostForce = transform.TransformDirection(Vector3.forward) * boost;
			Debug.Log (boostForce);
			col.GetComponent<Rigidbody>().AddForce (boostForce);
		}
	}
}
