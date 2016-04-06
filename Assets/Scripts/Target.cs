using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	private AudioSource audioSource;
	private ScoreKeeper scoreKeeper;

	[SerializeField]
	private int pointValue = 0;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		scoreKeeper = FindObjectOfType<ScoreKeeper>();
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.name == "Snowball" || collision.gameObject.name == "Snowball(Clone)") {
			// make noise
			audioSource.Play();

			//add pointValue to score
			scoreKeeper.AddScore(pointValue);
		}
	}
}
