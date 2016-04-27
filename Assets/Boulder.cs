using UnityEngine;
using System.Collections;

public class Boulder : MonoBehaviour {

	private SoundController soundController;

	private float previousBoulderRotation;

	[SerializeField]
	private float rollingSoundThreshold = 0;

	void Start () {
		soundController = GameObject.FindObjectOfType<SoundController>();

		previousBoulderRotation = transform.rotation.z;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (previousBoulderRotation > transform.rotation.z + rollingSoundThreshold || previousBoulderRotation < transform.rotation.z - rollingSoundThreshold) {
			if (!soundController.IsLooping()) {
				soundController.PlaySoundOnLoop(1);
			}
		} else if (soundController.IsLooping()) {
			soundController.EndPlayOnLoop();
		}

		previousBoulderRotation = transform.rotation.z;
	}
}
