using UnityEngine;
using System.Collections;

public class MovableObject : MonoBehaviour {

	public enum ObjectState {Neutral, Triggered, Interacting};

	[SerializeField]
	private SpriteRenderer outliner = null;
	[SerializeField]
	private Sprite origSprite = null;
	[SerializeField]
	private Sprite triggeredSprite = null;
	[SerializeField]
	private Sprite interSprite = null;

	[SerializeField]
	private ObjectState state = ObjectState.Neutral;

	private SpriteRenderer spriteRenderer;
	private Rigidbody2D objectRigidbody;

	private PlayerMovement playerMovement;
	private Transform playerTransform;
	private Rigidbody2D playerRigidbody;
	private Vector3 objectToPlayerVector;

	[SerializeField]
	private float clingPadding = 0.2f;

	// Use this for initialization
	void Start () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		objectRigidbody = gameObject.GetComponent<Rigidbody2D>();

		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
		playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		HandleMovement();
		ManageSprites();
	}

	private void HandleMovement() {
		if (state == ObjectState.Interacting) {
			objectToPlayerVector = playerTransform.position - transform.position;

			// positions box to follow player
			if (objectToPlayerVector.x < 0 && playerTransform.localScale.x < 0 || objectToPlayerVector.x > 0 && playerTransform.localScale.x > 0) {
				playerMovement.SetEncumbered(true);

				objectToPlayerVector = playerTransform.position - transform.position;

				if (objectToPlayerVector.x < -1) {
					objectToPlayerVector = new Vector3 (-spriteRenderer.bounds.extents.x - clingPadding, objectToPlayerVector.y, objectToPlayerVector.z);
					transform.position = new Vector3 (playerTransform.position.x - objectToPlayerVector.x, transform.position.y);

				} else if (objectToPlayerVector.x > 1) {
					objectToPlayerVector = new Vector3 (spriteRenderer.bounds.extents.x + clingPadding, objectToPlayerVector.y, objectToPlayerVector.z);
					transform.position = new Vector3 (playerTransform.position.x - objectToPlayerVector.x, transform.position.y);
				}

//				if (objectToPlayerVector.x < -1) {
//					objectToPlayerVector = new Vector3 (-1f, objectToPlayerVector.y, objectToPlayerVector.z);
//					transform.position = new Vector3 (playerTransform.position.x - objectToPlayerVector.x, transform.position.y);
//
//				} else if (objectToPlayerVector.x > 1) {
//					objectToPlayerVector = new Vector3 (1f, objectToPlayerVector.y, objectToPlayerVector.z);
//					transform.position = new Vector3 (playerTransform.position.x - objectToPlayerVector.x, transform.position.y);
//				}

				objectRigidbody.velocity = new Vector2 (playerRigidbody.velocity.x, objectRigidbody.velocity.y);
			} else {
				playerMovement.SetEncumbered(true);

				if (objectToPlayerVector.x < -1) {
					objectToPlayerVector = new Vector3 (-spriteRenderer.bounds.extents.x - clingPadding, objectToPlayerVector.y, objectToPlayerVector.z);
					transform.position = new Vector3 (playerTransform.position.x - objectToPlayerVector.x, transform.position.y);

				} else if (objectToPlayerVector.x > 1) {
					objectToPlayerVector = new Vector3 (spriteRenderer.bounds.extents.x + clingPadding, objectToPlayerVector.y, objectToPlayerVector.z);
					transform.position = new Vector3 (playerTransform.position.x - objectToPlayerVector.x, transform.position.y);

				}
			}

			// if player and object's 'y' coordinates differ too much, switch to "neutral" state
			if (playerMovement.gameObject.transform.position.y - transform.position.y > .6 || transform.position.y - playerMovement.gameObject.transform.position.y > .6) {
				state = ObjectState.Neutral;
			}
		}
	}

	private void ManageSprites() {
		if (state == ObjectState.Neutral) {
			outliner.sprite = origSprite;
		} else if (state == ObjectState.Triggered) {
			outliner.sprite = triggeredSprite;
		} else if (state == ObjectState.Interacting) {
			outliner.sprite = interSprite;
		}
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "Player" && state != ObjectState.Triggered && state != ObjectState.Interacting) {
			state = ObjectState.Triggered;
		}
	}

	void OnTriggerExit2D (Collider2D collider) {
		if (collider.gameObject.tag == "Player" && state != ObjectState.Interacting) {
			state = ObjectState.Neutral;

			playerMovement.SetEncumbered(false);
		}
	}

	public void SwapState () {
		if (state == ObjectState.Triggered) {
			state = ObjectState.Interacting;

		} else if (state == ObjectState.Interacting) {
			state = ObjectState.Triggered;

			playerMovement.SetEncumbered(false);
		}
	}

	public void ReactToPlayerJump () {
		if (state == ObjectState.Interacting) {
			state = ObjectState.Triggered;
		} else if (state == ObjectState.Triggered) {
			state = ObjectState.Neutral;
		}
	}
}
