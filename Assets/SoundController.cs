using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	private AudioSource controllerAudioSource = null;

	[SerializeField]
	private AudioClip[] audioClips = null;

	private bool isLooping = false;
	private float originalVolume = 0;


	// Use this for initialization
	void Start () {
		controllerAudioSource = GetComponent<AudioSource>();

		originalVolume = controllerAudioSource.volume;
	}

	public void PlaySoundEffect (int soundNumber = 0) {
		if (audioClips[soundNumber].name == "Water Splash") {
			controllerAudioSource.volume = 0.5f;
		} else {
			controllerAudioSource.volume = originalVolume;
		}

		controllerAudioSource.PlayOneShot(audioClips[soundNumber]);
	}

	public void PlaySoundOnLoop (int soundNumber = 0) {
		controllerAudioSource.volume = originalVolume;
		
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
