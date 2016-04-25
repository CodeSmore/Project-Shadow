using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {

	private SoundController soundController;

	// Use this for initialization
	void Start () {
		soundController = GameObject.FindObjectOfType<SoundController>();
	}

	void OnCollisionEnter2D (Collision2D collider) {
		if (collider.gameObject.tag == "PlayerProjectile") {
			soundController.PlaySoundEffect(0);
		}
	}
}
