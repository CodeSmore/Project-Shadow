using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadLevel (string sceneName) {
		SceneManager.LoadScene(sceneName);
	}

	public void LoadNextScene () {
		int sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;

		if (sceneToLoad >= SceneManager.sceneCountInBuildSettings) {
			sceneToLoad = 0;
		}

		SceneManager.LoadScene(sceneToLoad);
	}

	public void ResetLevel () {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
