using UnityEngine;
using System.Collections;

public class BoulderInteractCollider : MonoBehaviour {

	[SerializeField]
	private Transform boulderObjectTransform = null;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position != boulderObjectTransform.position) {
			transform.position = boulderObjectTransform.position;
		}
	}
}
