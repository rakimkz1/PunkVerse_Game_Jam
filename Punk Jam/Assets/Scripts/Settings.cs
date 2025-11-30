using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

	public static Settings instance;
	[Header("Sliders")]
	public Slider musicVolumeSlider;
	public Slider sfxVolumeSlider;
	public Slider mouseSensitivitySlider;

	[Header("Events")]
	public Action<float> OnMusicVolumeChanged;
	public Action<float> OnSFXVolumeChanged;
	public Action<float> OnMouseSensitivityChanged;

	private void Awake()
	{
		instance = this;
		// Load saved values (default 1 for volume, 0.5f for sensitivity)
		float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
		float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
		float sensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 0.8f);

		// Sync UI
		musicVolumeSlider.value = musicVolume;
		sfxVolumeSlider.value = sfxVolume;
		mouseSensitivitySlider.value = sensitivity;

		// Subscribe to UI events
		musicVolumeSlider.onValueChanged.AddListener(HandleMusicVolume);
		sfxVolumeSlider.onValueChanged.AddListener(HandleSFXVolume);
		mouseSensitivitySlider.onValueChanged.AddListener(HandleSensitivity);
	}

	private void HandleMusicVolume(float value)
	{
		PlayerPrefs.SetFloat("MusicVolume", value);
		OnMusicVolumeChanged?.Invoke(value);
	}

	private void HandleSFXVolume(float value)
	{
		PlayerPrefs.SetFloat("SFXVolume", value);
		OnSFXVolumeChanged?.Invoke(value);
	}

	private void HandleSensitivity(float value)
	{
		PlayerPrefs.SetFloat("MouseSensitivity", value);
		OnMouseSensitivityChanged?.Invoke(value);
	}
}
