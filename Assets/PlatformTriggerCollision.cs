using UnityEngine;
using System.Collections;

public class PlatformTriggerCollision : MonoBehaviour {

	[SerializeField]
	private BoxCollider2D[] platformColliders = null;
	[SerializeField]
	private BoxCollider2D[] platformTriggers = null;

	private Collider2D[] playerColliders;

	private float triggerCount = 0;

	// Use this for initialization
	void Start () {
		playerColliders = GameObject.Find("Player").GetComponents<Collider2D>();

		// platform trigger ignores it's collider
		foreach (BoxCollider2D boxCollider in platformColliders) {
			foreach (BoxCollider2D boxTrigger in platformTriggers) {
				Physics2D.IgnoreCollision(boxCollider, boxTrigger, true);
			}
		}
	}
	
	void OnTriggerEnter2D (Collider2D collider) {
		triggerCount++;
		if (collider.gameObject.name == "Player") {
			foreach (BoxCollider2D boxCollider in platformColliders) {
				foreach (Collider2D playerCollider in playerColliders) {
					Physics2D.IgnoreCollision(boxCollider, playerCollider, true);
					boxCollider.gameObject.layer = 2;
				}
			} 
		}
	}

	void OnTriggerExit2D (Collider2D collider) {
		triggerCount--;
		if (collider.gameObject.name == "Player" && triggerCount == 0) {
			foreach (BoxCollider2D boxCollider in platformColliders) {
				foreach (Collider2D playerCollider in playerColliders) {
					Physics2D.IgnoreCollision(boxCollider, playerCollider, false);
					boxCollider.gameObject.layer = 8;
				}
			} 
		}
	}
}
