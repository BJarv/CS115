using UnityEngine;
using System.Collections;

public class InAttackRange : MonoBehaviour {

	public bool colliding = false;
	public Collider colWith;

	void OnTriggerEnters(Collider obj) {
		if(obj.gameObject != transform.parent.transform.parent.gameObject && obj.tag == "Player"){ //if gameobject is not itself but still a player
			colliding = true;
			colWith = obj;
		}

	}

	void OnTriggerExit(Collider obj) {
		if(obj.gameObject != transform.parent.transform.parent.gameObject && obj.tag == "Player"){ //if gameobject is not itself but still a player
			colliding = false;
			colWith = null;
		}
	}

}
