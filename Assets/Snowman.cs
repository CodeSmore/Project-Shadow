using UnityEngine;
using System.Collections;

public class Snowman : MonoBehaviour {

	[SerializeField]
	private GameObject snowManHead = null;
	[SerializeField]
	private TriggerController trigger = null;

	void OnDestroy () {
		if (trigger.IsTriggered()) {
			Instantiate(snowManHead, transform.position, Quaternion.identity);
		}
	}
}
