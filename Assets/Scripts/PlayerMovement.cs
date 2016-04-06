﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
	private float movementSpeed = 0;
	[SerializeField]
	private float encumberedMovementSpeed = 0;
	[SerializeField]
	private bool encumbered = false;
	[SerializeField]
	private bool RestrictedJump = false;
	[SerializeField]
	private Transform[] groundpoints = null;
	[SerializeField]
	private float groundRadius = 0;
	[SerializeField]
	private float jumpForce = 0;
	[SerializeField]
	private LayerMask whatIsGround = 0;

	private Rigidbody2D playerRigidbody;
	private Animator playerAnimator;

	private bool facingRight;
	private bool isGrounded;
	private bool movementEnabled = true;

	private float horizontal;

	private Vector3 startPosition;
 
	// Use this for initialization
	void Start () {
		playerRigidbody = GetComponent<Rigidbody2D>();
		playerAnimator = GetComponent<Animator>();

		startPosition = transform.position;
	}

	void Update () {
		HandleKeyboard();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		isGrounded = CheckIsGrounded();

		HandleMovement(horizontal);
		HandleAnimationLayers();
	}

	private void HandleMovement(float horizontalDirection) {
		if (movementEnabled) {
			if (isGrounded || !RestrictedJump) {
				
				float speed;

				if (encumbered) {
					speed = encumberedMovementSpeed;
				} else {
					speed = movementSpeed;
				}

				playerRigidbody.velocity = new Vector2(horizontalDirection * speed, playerRigidbody.velocity.y);
			}
			FlipImage(horizontalDirection);

			playerAnimator.SetFloat("speed", Mathf.Abs(horizontalDirection));

			if (playerRigidbody.velocity.y < 0 && !isGrounded) {
				playerAnimator.SetBool("land", false);
				playerAnimator.SetBool("fall", true);
			}
		} else {
			playerAnimator.SetFloat("speed", 0);

			// handle when player is on ledge
			if (playerAnimator.GetBool("ledge grab")) {
				if (gameObject.transform.localScale.x < 0 && horizontalDirection > 0/*facing left and pressing right*/ 
					|| gameObject.transform.localScale.x > 0 && horizontalDirection < 0/*facing right and pressing left*/) {
					ReleaseLedge ();
				} else if (gameObject.transform.localScale.x < 0 && horizontalDirection < 0/*facing left and pressing left*/ 
						   || gameObject.transform.localScale.x > 0 && horizontalDirection > 0/*facing right and pressing right*/) {
					ClimbLedge();
				}
			}
		}
	}

	private void FlipImage (float horizontalDirection) {
		if (horizontalDirection > 0 && facingRight || horizontalDirection < 0 && !facingRight) {
			facingRight = !facingRight;

			Vector3 theScale = transform.localScale;

			theScale.x *= -1;

			transform.localScale = theScale;
		}
	}

	private bool CheckIsGrounded () {
		
		foreach (Transform point in groundpoints) {
			Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

			foreach (Collider2D collider in colliders) {
				if (collider.gameObject != gameObject) {
					playerAnimator.SetBool("fall", false);
					playerAnimator.SetBool("land", true);
					return true;
				}
			}
		}

		return false;
	}

	private void HandleAnimationLayers () {
		if (!isGrounded) {
			playerAnimator.SetLayerWeight(1, 1);
		} else {
			playerAnimator.SetLayerWeight(1, 0);
		}
	}

	private void HandleKeyboard () {
		horizontal = Input.GetAxis("Horizontal");

		if (Input.GetKeyDown(KeyCode.Space)) {
			Jump();
		}
	}

	public void MoveLeft () {
		horizontal = -1;
	}

	public void MoveRight () {
		horizontal = 1;
	}

	public void ResetMovement () {
		horizontal = 0;
	}

	public void Jump () {

		if (isGrounded && movementEnabled) {
			playerAnimator.SetBool("land", false);
			playerRigidbody.AddForce(new Vector2 (0, jumpForce));
			playerAnimator.SetTrigger("jump");

			// reset all interactables
			MovableObject[] interactables = GameObject.FindObjectsOfType<MovableObject>();
			foreach (MovableObject inter in interactables) {
				inter.ReactToPlayerJump();
			}
		} 
	}

	public void ResetPosition () {
		transform.position = startPosition + new Vector3 (0, .25f, 0);
		ResetMovement();

		// reset velocity
		playerRigidbody.velocity = Vector3.zero;
		// if facing left, switch to right
		if (transform.localScale.x == -1) {
			FlipImage(1);
		}
	}

	public void SetEncumbered (bool isEncumbered) {
		encumbered = isEncumbered;
	}

	public bool IsGrounded() {
		return isGrounded;
	}

	public void DisableAllMovement () {
		movementEnabled = false;
	}
	public void EnableAllMovement () {
		movementEnabled = true;
	}

	public void DisableClimb () {
		playerAnimator.SetBool("climb", false);
	}

	public void ResetVeloctiy () {
		playerRigidbody.velocity = Vector3.zero;
		playerRigidbody.angularVelocity = 0f;
	}

	public void ReleaseLedge () {
		playerRigidbody.drag = 0f;
		EnableAllMovement();
		FlipImage(horizontal);
		playerAnimator.SetBool("ledge grab", false);
	}

	public void ClimbLedge () {
		playerAnimator.SetTrigger("climb");
	}

	public void TeleportClimb () { 
		transform.position = new Vector2 (transform.position.x + 0.25f * transform.localScale.x, transform.position.y + 1.75f);
	}
}