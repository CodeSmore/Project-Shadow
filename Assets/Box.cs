using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	private SoundController soundController;

	private float previousBoxPositionX = 0;

	[SerializeField]
	private Transform[] groundPoints = null;
	[SerializeField]
	private float groundRadius = 0;
	[SerializeField]
	private LayerMask whatIsGround = -1;

	[SerializeField]
	private float movingSoundThreshold = 0;

	// Use this for initialization
	void Start () {
		soundController = GetComponentInChildren<SoundController>();

		previousBoxPositionX = transform.position.x;
	}

	void FixedUpdate () {
		if (IsGrounded() && (previousBoxPositionX > transform.position.x + movingSoundThreshold || previousBoxPositionX < transform.position.x - movingSoundThreshold)) {
			if (!soundController.IsLooping()) {
				soundController.PlaySoundOnLoop(0);
			}
		} else if (soundController.IsLooping()) {
			soundController.EndPlayOnLoop();
		}

		previousBoxPositionX = transform.position.x;
	}

	private bool IsGrounded () {
		
		foreach (Transform point in groundPoints) {

			Collider2D[] colliders = null;
			colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

			foreach (Collider2D collider in colliders) {
				if (collider.gameObject != gameObject) {
					return true;
				}
			}
		}

		return false;
	}
}
