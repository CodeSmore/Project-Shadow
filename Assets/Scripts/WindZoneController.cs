using UnityEngine;
using System.Collections;

public class WindZoneController : MonoBehaviour {

	[SerializeField]
	private float windForce = 0;
	[SerializeField]
	private bool variableWind = false;
	[SerializeField]
	private float enabledTime = 0;
	[SerializeField]
	private float disabledTime = 0;

	[SerializeField]
	private AudioClip highWindSound = null;
	[SerializeField]
	private AudioClip lowWindSound = null;

	private SpriteRenderer spriteRenderer;
	private AudioSource windAudioSource;

	private bool windEnabled = true;
	private float windTimer = 0;

	void Start () {
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();	
		windAudioSource = GetComponent<AudioSource>();
	}

	void Update () {
		if (variableWind) {
			windTimer += Time.deltaTime;

			if (windTimer < enabledTime) {
				windEnabled = true;
				windAudioSource.clip = highWindSound;
			} else if (windTimer < enabledTime + disabledTime) {
				windEnabled = false;
				windAudioSource.clip = lowWindSound;
			} else {
				windTimer = 0;
			}

			if (!windAudioSource.isPlaying) {
				windAudioSource.Play();
			}

			ManageColor();
		}
	}
	void OnTriggerStay2D (Collider2D collider) {
		if (windEnabled && collider.gameObject.tag != "Environment" && collider.gameObject.GetComponent<Rigidbody2D>()) {
			collider.GetComponent<Rigidbody2D>().AddForce(Vector2.left * windForce);
		}
	}

	void ManageColor () {
		if (windEnabled) {
			// purple
			spriteRenderer.color = new Color (0.6f, 0.6f, 1f, 0.4f);
		} else {
			// sky blue
			spriteRenderer.color = new Color (0.6f, 1f, 1f, 0.4f);
		}
	}

	void OnDrawGizmos () {
		BoxCollider2D[] boxColliders = GetComponents<BoxCollider2D>();

		foreach (BoxCollider2D collider in boxColliders) {
			Vector2 offset = collider.offset;

			// Draw BoxCollider
			if (collider != null) {
				Vector2 tl = new Vector2(transform.position.x - (collider.size.x / 2), 
											transform.position.y + (collider.size.y / 2)) + offset;
				Vector2 bl = new Vector2(transform.position.x - (collider.size.x / 2), 
											transform.position.y - (collider.size.y / 2)) + offset;
				Vector2 br = new Vector2(transform.position.x + (collider.size.x / 2), 
											transform.position.y - (collider.size.y / 2)) + offset;
				Vector2 tr = new Vector2(transform.position.x + (collider.size.x / 2), 
											transform.position.y + (collider.size.y / 2)) + offset;

				Gizmos.color = Color.red;

				Gizmos.DrawLine(tl, bl);
				Gizmos.DrawLine(bl, br);
				Gizmos.DrawLine(br, tr);
				Gizmos.DrawLine(tr, tl);
			}
		}

	}
}
