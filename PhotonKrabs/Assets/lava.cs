using UnityEngine;
using System.Collections;

public class lava : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player") {
			col.transform.gameObject.GetComponent<PhotonView>().RPC ("TakeDamage", PhotonTargets.AllBuffered, 1f);
		}
	}
}
