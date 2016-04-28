using UnityEngine;
using System.Collections;

public class DisappearFromTrigger : MonoBehaviour {

	[SerializeField]
	private TriggerController trigger = null;
	
	// Update is called once per frame
	void Update () {
		if (trigger.IsTriggered()) {
			Destroy(gameObject);
		}
	}
}
