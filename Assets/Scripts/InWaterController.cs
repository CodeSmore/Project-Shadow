using UnityEngine;
using System.Collections;

public class InWaterController : MonoBehaviour {
	private PlayerMovement playerMovement;
	private Rigidbody2D playerRigidbody2D;
	private Camera playerCamera;

	// Use this for initialization
	void Start () {
		playerMovement = GetComponent<PlayerMovement>();
		playerRigidbody2D = GetComponent<Rigidbody2D>();
		playerCamera = Camera.main;
	}
	
	void OnTriggerStay2D (Collider2D collider) {
		if (collider.gameObject.layer == 4 /*water*/) {
			float waterSurfaceYPos = collider.transform.position.y + collider.gameObject.GetComponent<BoxCollider2D>().size.y / 2 + collider.gameObject.GetComponent<BoxCollider2D>().offset.y;

			if (gameObject.transform.position.y < waterSurfaceYPos) {
				// simulate water
				playerMovement.DisableAllMovement();
				playerRigidbody2D.gravityScale = 0.3f;
				playerRigidbody2D.drag = 1;

				// detach camera
				playerCamera.gameObject.transform.parent = null;
			}

		}
	}

	void OnTriggerExit2D (Collider2D collider) {
		if (collider.gameObject.layer == 4 /*water*/) {
			// simulate water
			playerMovement.EnableAllMovement();
			playerRigidbody2D.gravityScale = 1f;
			playerRigidbody2D.drag = 0;

			// attach camera
			playerCamera.gameObject.transform.parent = gameObject.transform;
			playerCamera.transform.localPosition = new Vector3 (0f, 0f, -10f);
		}
	}
}
