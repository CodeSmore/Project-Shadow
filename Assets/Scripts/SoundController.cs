using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	private AudioSource controllerAudioSource = null;

	[SerializeField]
	private AudioClip[] audioClips = null;

	private bool isLooping = false;

	// Use this for initialization
	void Start () {
		controllerAudioSource = GetComponent<AudioSource>();
	}

	public void PlaySoundEffect (int soundNumber = 0) {
		controllerAudioSource.PlayOneShot(audioClips[soundNumber]);
	}

	public void PlaySoundOnLoop (int soundNumber = 0) {		
		controllerAudioSource.clip = audioClips[soundNumber];
		controllerAudioSource.loop = true;
		controllerAudioSource.Play();

		isLooping = true;
	}

	public void EndPlayOnLoop () {
		controllerAudioSource.loop = false;
		controllerAudioSource.Stop();

		isLooping = false;
	}

	public bool IsLooping () {
		return isLooping;
	}
}
