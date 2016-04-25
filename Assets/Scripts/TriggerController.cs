using UnityEngine;
using System.Collections;

public class TriggerController : MonoBehaviour {

	[SerializeField]
	private bool trigger = false;
	[SerializeField]
	private bool soundTrigger = false;
	[SerializeField]
	private int soundNumToTrigger = 0;
	[SerializeField]
	private string[] tagsThatTrigger = null;

	private SoundController soundController;

	void Start () {
		soundController = GameObject.FindObjectOfType<SoundController>();
	}

	void OnTriggerEnter2D (Collider2D collider) {
		foreach (string tag in tagsThatTrigger) {
			if (collider.gameObject.tag == tag) {
				trigger = true;

				if (soundTrigger) {
					soundController.PlaySoundEffect(soundNumToTrigger);
				}
			}
		}
	}

	public bool IsTriggered () {
		return trigger;
	}
}
