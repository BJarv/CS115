using UnityEngine;
using System.Collections;

public class lava : MonoBehaviour {

	public AudioClip burn_audio;

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player") {
			AudioSource.PlayClipAtPoint (burn_audio, col.transform.position);
			col.transform.gameObject.GetComponent<PhotonView>().RPC ("TakeDamage", PhotonTargets.AllBuffered, 1f, "Lava");
		}
	}
}
