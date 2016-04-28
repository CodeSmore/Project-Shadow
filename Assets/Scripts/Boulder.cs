using UnityEngine;
using System.Collections;

public class Boulder : MonoBehaviour {

	private SoundController soundController;
	private LevelManager levelManager;
	private Rigidbody2D boulderRigidbody;

	private float previousBoulderRotation;

	[SerializeField]
	private bool isDangerous = false;
	private bool dangerIsActive = false;

	[SerializeField]
	private float rollingSoundThreshold = 0;
	[SerializeField]
	private float dangerousVelocity = 0;

	void Start () {
		soundController = GameObject.FindObjectOfType<SoundController>();
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		boulderRigidbody = GetComponent<Rigidbody2D>();

		previousBoulderRotation = transform.rotation.z;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float boulderVelocity = Mathf.Abs(boulderRigidbody.velocity.x) + Mathf.Abs(boulderRigidbody.velocity.y);

		if (previousBoulderRotation > transform.rotation.z + rollingSoundThreshold || previousBoulderRotation < transform.rotation.z - rollingSoundThreshold || boulderVelocity > dangerousVelocity) {
			if (!soundController.IsLooping()) {
				soundController.PlaySoundOnLoop(1);
			}

			if (isDangerous) {
				dangerIsActive = true;
			}
		} else if (soundController.IsLooping()) {
			soundController.EndPlayOnLoop();

			if (isDangerous) {
				dangerIsActive = false;
			}
		}

		previousBoulderRotation = transform.rotation.z;
	}

	void OnTriggerEnter2D (Collider2D collider) {
		Debug.Log(collider);
		if (collider.gameObject.tag == "Player" && dangerIsActive) {
			levelManager.ResetLevel();
		}
	}
}
