using UnityEngine;
using System.Collections;

public class TriggerController : MonoBehaviour {

	[SerializeField]
	private bool trigger = false;
	[SerializeField]
	private string[] tagsThatTrigger = null;

	void OnTriggerEnter2D (Collider2D collider) {
		foreach (string tag in tagsThatTrigger) {
			if (collider.gameObject.tag == tag) {
				trigger = true;
			}
		}
	}

	public bool IsTriggered () {
		return trigger;
	}
}
