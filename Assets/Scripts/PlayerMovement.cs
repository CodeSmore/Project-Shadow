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
	private Transform[] groundPoints = null;
	[SerializeField]
	private float groundRadius = 0;
	[SerializeField]
	private float jumpForce = 0;
	[SerializeField]
	private LayerMask whatIsGround = -1;

	private Rigidbody2D playerRigidbody;
	private Animator playerAnimator;
	private PlayerSoundController playerSoundController;

	private bool facingRight;

	[SerializeField]
	private bool isGrounded;
	private bool movementEnabled = true;
	private string groundTag;

	private float horizontal;

	private Vector3 startPosition;

	[SerializeField]
	private float holdToClimbLimit = 0;
	private float holdToClimbTimer = 0;

	private float startJumpYPos = 0;
	private float endJumpYPos = 0;
	[SerializeField]
	private float hardLandingThreshold = 0;
 
	// Use this for initialization
	void Start () {
		playerRigidbody = GetComponent<Rigidbody2D>();
		playerAnimator = GetComponent<Animator>();
		playerSoundController = GetComponent<PlayerSoundController>();

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

			// handle when player is on ledge
			if (playerAnimator.GetBool("ledge grab")) {
				if (gameObject.transform.localScale.x < 0 && horizontalDirection < 0/*facing left and pressing left*/ 
						   || gameObject.transform.localScale.x > 0 && horizontalDirection > 0/*facing right and pressing right*/) {
					// timer in place b/c climb is called even if player chooses to release ledge
					holdToClimbTimer += Time.deltaTime;

					if (holdToClimbTimer > holdToClimbLimit) {
						ClimbLedge();
					}
				} else if (gameObject.transform.localScale.x < 0 && horizontalDirection > 0/*facing left and pressing right*/ 
					|| gameObject.transform.localScale.x > 0 && horizontalDirection < 0/*facing right and pressing left*/) {
					ReleaseLedge ();
				}
			}
		} else {
			playerAnimator.SetFloat("speed", 0);
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
		
		foreach (Transform point in groundPoints) {

			Collider2D[] colliders = null;
			colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

			foreach (Collider2D collider in colliders) {
				if (collider.gameObject != gameObject) {

					// corrects bump up when moving up slope
					if (playerRigidbody.velocity.y > 0 && playerAnimator.GetBool("jump") == false) {
						playerRigidbody.velocity = new Vector2 (playerRigidbody.velocity.x, 0);
					}

					// included to assist above statement (originally was a trigger)
					if (playerRigidbody.velocity.y < 0) {
						playerAnimator.SetBool("jump", false);
						endJumpYPos = transform.position.y;

						// decide to play hard landing or not
						if ((startJumpYPos - endJumpYPos) > hardLandingThreshold) {
							// play sound
							playerSoundController.PlayHardLandingSound();

							startJumpYPos = endJumpYPos = transform.position.y;
						}
					}


					playerAnimator.SetBool("fall", false);
					playerAnimator.SetBool("land", true);


					groundTag = collider.gameObject.tag;
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
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			// move right
			horizontal = 1;
		} else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			// move left
			horizontal = -1;
		} 

		if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) {
			horizontal = 0;
		}

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

		if (isGrounded && movementEnabled /*&& playerRigidbody.velocity.y == 0 TODO too restrictive, build range*/) {
			playerAnimator.SetBool("land", false);
			playerRigidbody.AddForce(new Vector2 (0, jumpForce));
			playerAnimator.SetBool("jump", true);

			// play 'jump' sound effect
			playerSoundController.PlayJumpingSound();


			// reset all interactables
			MovableObject[] interactables = GameObject.FindObjectsOfType<MovableObject>();
			foreach (MovableObject inter in interactables) {
				inter.ReactToPlayerJump();
			}

			startJumpYPos = transform.position.y;
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
		holdToClimbTimer = 0;
	}

	public void ResetVeloctiy () {
		playerRigidbody.velocity = Vector3.zero;
		playerRigidbody.angularVelocity = 0f;
	}

	public void ReleaseLedge () {
		playerRigidbody.drag = 0f;
		playerAnimator.SetBool("ledge grab", false);
		FlipImage(horizontal);
	}

	private void ClimbLedge () {
		if (playerAnimator.GetBool("ledge grab")) {
			playerAnimator.SetBool("climb", true);
		}
	}

	public void TeleportClimb () { 
		transform.position = new Vector2 (transform.position.x + 0.25f * transform.localScale.x, transform.position.y + 1.75f);
	}

	public float GetHorizontal () {
		return horizontal;
	}

	public string GetGroundTag () {
		return groundTag;
	}
}
