using UnityEngine;
using System.Collections;

public class ColapsibleFloor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D collider) {
		Rigidbody2D triggerRigidbody = collider.gameObject.GetComponent<Rigidbody2D>();
		if (triggerRigidbody && triggerRigidbody.mass > 50) {
			triggerRigidbody.isKinematic = false;
			collider.GetComponent<MonsterController>().DisableWalk();
			Destroy(gameObject);
		}
	}
}
