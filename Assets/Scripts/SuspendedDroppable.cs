using UnityEngine;
using System.Collections;

public class SuspendedDroppable : MonoBehaviour {

	[SerializeField]
	private TriggerController icicleDrop = null;

	private Rigidbody2D icicleRigidbody;

	// Use this for initialization
	void Start () {
		icicleRigidbody = GetComponent<Rigidbody2D>(); 
	}
	
	// Update is called once per frame
	void Update () {
		if (icicleDrop.IsTriggered()) {
			// make velocity down
			icicleRigidbody.isKinematic = false;
		}
	}

	// if grounded or OnTriggerEnter, destroy or animate and destroy
}
