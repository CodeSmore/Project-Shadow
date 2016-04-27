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
		soundController = GameObject.Find("SoundController").GetComponent<SoundController>();
	}

	void OnTriggerEnter2D (Collider2D collider) {
		foreach (string tag in tagsThatTrigger) {
			if (collider.gameObject.tag == tag) {
				if (soundTrigger && trigger == false) {
					soundController.PlaySoundEffect(soundNumToTrigger);
				}
				trigger = true;
			}
		}
	}

	public bool IsTriggered () {
		return trigger;
	}
}
