using UnityEngine;
using System.Collections;

public class PlayerInteractableController : MonoBehaviour {

	private Animator playerAnimator;

	// Use this for initialization
	void Start () {
		playerAnimator = GetComponent<Animator>();
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "Throwable") {
			playerAnimator.SetBool("firing", true);
		}
	}

	void OnTriggerExit2D (Collider2D collider) {
		if (collider.gameObject.tag == "Throwable") {
			playerAnimator.SetBool("firing", false);
		}
	}
}
