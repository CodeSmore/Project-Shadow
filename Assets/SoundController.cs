using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	[SerializeField]
	private AudioSource controllerAudioSource = null;

	[SerializeField]
	private AudioClip[] audioClips = null;


	// Use this for initialization
	void Start () {
	}

	public void PlaySoundEffect (int soundNumber = 0) {
		
		controllerAudioSource.PlayOneShot(audioClips[soundNumber]);
	}
}
