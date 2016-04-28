using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

	private GameObject menuPanel = null;
	private Slider musicSlider = null;
	private Slider soundEffectsSlider = null;

	private Text soundEffectsLevelText = null;
	private Text musicLevelText = null;

	private AudioSource[] allAudioSources = null;

	// Use this for initialization
	void Start () {
		menuPanel = GameObject.Find("Menu Panel");

		allAudioSources = GameObject.FindObjectsOfType<AudioSource>();

		musicSlider = GameObject.Find("Music Slider").GetComponent<Slider>();
		soundEffectsSlider = GameObject.Find("Sound Effects Slider").GetComponent<Slider>();

		soundEffectsLevelText = GameObject.Find("Sound Effects Level Text").GetComponent<Text>();
		musicLevelText = GameObject.Find("Music Level Text").GetComponent<Text>();

		UpdateSliders();
		UpdateVolumes();

		menuPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		UpdateVolumes();
	}

	public void ToggleMenu () {
		if (menuPanel.activeSelf) {
			//save slider data to player prefs
			UpdateVolumes();
			PlayerPrefsManager.Save(musicSlider.value, soundEffectsSlider.value);
		}

		menuPanel.SetActive(!menuPanel.activeSelf);
	}

	private void UpdateSliders () {
		musicSlider.value = PlayerPrefsManager.GetMusicVolume();
		soundEffectsSlider.value = PlayerPrefsManager.GetSoundEffectsVolume();
	}

	public void UpdateVolumes () {
		foreach (AudioSource source in allAudioSources) {
			if (source.gameObject.tag == "Music") {
				source.volume = musicSlider.value;
			} else {
				source.volume = soundEffectsSlider.value;
			}
		}

		soundEffectsLevelText.text = Mathf.RoundToInt(soundEffectsSlider.value * 100).ToString();
		musicLevelText.text = Mathf.RoundToInt(musicSlider.value * 100).ToString();
	}
}
