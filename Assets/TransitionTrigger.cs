using UnityEngine;
using System.Collections;

public class TransitionTrigger : MonoBehaviour {

	private LevelManager levelManager;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.name == "Player") {
			levelManager.LoadNextScene();
		}
	}
}
