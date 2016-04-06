using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	private PlayerMovement playerMovement;

	void Start () {
		playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag == "Player") {
			playerMovement.ResetPosition();
		}
	}
}
