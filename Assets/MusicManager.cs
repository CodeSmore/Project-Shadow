using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	private static MusicManager instance = null;

	void Awake () {
		// Logs which Music Player is present at Awake() based on ID
//		Debug.Log ("Music player Awake: " +GetInstanceID());
		
		// If 'instance' possess as a value, there must be > 1 Music_Player GameObject, therefore, destroy 'this'.
		if (instance != null) {
			Destroy (gameObject);
//			print ("Duplicate music player self-destructing!");
		// Else, set 'instance' to 'this'
		} else {
			instance = this;
			
			// Keeps 'this' alive when new scene is loaded.
			GameObject.DontDestroyOnLoad(instance);
		}
	}
}
