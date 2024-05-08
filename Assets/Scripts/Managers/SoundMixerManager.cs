using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
	[SerializeField] private AudioMixer audioMixer;
	[SerializeField] private Slider masterVolumeSlider;
	[SerializeField] private Slider musicVolumeSlider;
	[SerializeField] private Slider soundFXVolumeSlider;

	private void Start()
	{
		if(PlayerPrefs.HasKey("MusicVolume") && PlayerPrefs.HasKey("MusicVolume") && PlayerPrefs.HasKey("SoundFXVolume"))
		{
			LoadVolume();
		}
		else
		{
			SetMasterVolume();
			SetMusicVolume();
			SetSoundFXVolume();
		}
	}

	public void SetMasterVolume()
	{	
		float level = masterVolumeSlider.value;
		audioMixer.SetFloat("MasterVolume",Mathf.Log10(level)*20);
		PlayerPrefs.SetFloat("MasterVolume",level);
	}

	public void SetMusicVolume()
	{
		float level = musicVolumeSlider.value;
		audioMixer.SetFloat("MusicVolume",Mathf.Log10(level)*20);
		PlayerPrefs.SetFloat("MusicVolume",level);
	}

	public void SetSoundFXVolume()
	{
		float level = soundFXVolumeSlider.value;
		audioMixer.SetFloat("SoundFXVolume",Mathf.Log10(level)*20);
		PlayerPrefs.SetFloat("SoundFXVolume",level);
	}

	private void LoadVolume()
	{
		masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
		musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
		soundFXVolumeSlider.value = PlayerPrefs.GetFloat("SoundFXVolume");
	}
}
