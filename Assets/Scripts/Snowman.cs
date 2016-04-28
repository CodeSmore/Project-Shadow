using UnityEngine;
using System.Collections;

public class Snowman : MonoBehaviour {

	[SerializeField]
	private GameObject snowManHead = null;
	[SerializeField]
	private Transform headSpawnPoint = null;
	[SerializeField]
	private TriggerController trigger = null;

	void OnDestroy () {
		if (trigger.IsTriggered()) {
			Instantiate(snowManHead, headSpawnPoint.position, Quaternion.identity);
		}
	}
}
