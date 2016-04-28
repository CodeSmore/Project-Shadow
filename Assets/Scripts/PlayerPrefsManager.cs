using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

	private static string MUSIC_VOLUME_KEY = "music_volume";
	private static string SOUND_EFFECTS_VOLUME_KEY = "sound_effects_volume";

	public static void Save (float musicVolume, float soundEffectsVolume) {
		PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, musicVolume);
		PlayerPrefs.SetFloat(SOUND_EFFECTS_VOLUME_KEY, soundEffectsVolume);
	}

	public static float GetMusicVolume () {
		float volume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1);

		return volume;
	}

	public static float GetSoundEffectsVolume () {
		float volume = PlayerPrefs.GetFloat(SOUND_EFFECTS_VOLUME_KEY, 1);

		return volume;
	}
}


