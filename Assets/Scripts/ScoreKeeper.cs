using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	[SerializeField]
	private Text scoreText = null;
	[SerializeField]
	private GameObject floor = null;

	private int currentScore = 0;

	void UpdateScore () {
		scoreText.text = "Score: " + currentScore;

		if (currentScore >= 100) {
			// make floor disappear!
			floor.SetActive(false);
		}
	}

	public void AddScore (int addition) {
		currentScore += addition;

		UpdateScore();
	}
}
