using UnityEngine;
using System.Collections;

public class PlayerSoundController : MonoBehaviour {

	[SerializeField]
	private AudioSource playerAudioSource = null;
	[SerializeField]
	private AudioSource oneShotAudioSource = null;

	[SerializeField]
	private AudioClip runningSnow = null;
	[SerializeField]
	private AudioClip runningWood = null;
	[SerializeField]
	private AudioClip runningStone = null;
	[SerializeField]
	private AudioClip landing = null;

	private PlayerMovement playerMovement;

	// Use this for initialization
	void Start () {
		playerMovement = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerMovement.GetHorizontal() != 0 && playerMovement.IsGrounded()) {

			if (playerMovement.GetGroundTag() == "Wood") {
				playerAudioSource.clip = runningWood;
			} else if (playerMovement.GetGroundTag() == "Stone") {
				playerAudioSource.clip = runningStone;
			}else {
				playerAudioSource.clip = runningSnow;
			}

			if (playerAudioSource.isPlaying == false) {
				playerAudioSource.Play();
			}
		} else {
			playerAudioSource.Stop();
		}
	}

	public void PlayLandingSound () {
		oneShotAudioSource.PlayOneShot(landing);
	}
}
