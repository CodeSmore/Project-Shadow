using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	private LevelManager levelManager;

	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag == "Player") {
			levelManager.ResetLevel();
		} else {
			Destroy(collider.gameObject);
		}
	}
}
