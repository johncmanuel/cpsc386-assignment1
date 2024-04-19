using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer mainAudioMixer;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider SFXVolumeSlider;

    // Value in decibels
    private const float DefaultVolume = -15f;

    // Start off as active, then deactivate after loading volume values from Prefs
    private void Start()
    {
        if (mainAudioMixer == null)
            Debug.LogError("Main AudioMixer not set in the inspector");

        if (masterVolumeSlider == null)
            Debug.LogError("Master volume slider not found");

        InitializeVolume();

        // gameObject.SetActive(false);
        var settingsScrollView = GameObject.Find("Settings Scroll View");
        settingsScrollView.SetActive(false);
    }

    // private void OnDisable()
    // {
    //     PlayerPrefs.Save();
    // }

    private void InitializeVolume()
    {
        // float masterVolumePrefs = PlayerPrefs.GetFloat(AudioNames.MasterVolume);
        // if (masterVolumePrefs == -1f)
        // {
        //     Debug.Log("Master volume not found in prefs, setting to default");
        //     mainAudioMixer.SetFloat(AudioNames.MasterVolume, DefaultVolume);
        // }
        // else
        // {
        //     Debug.Log("Master volume found in prefs, setting to " + masterVolumePrefs);
        //     mainAudioMixer.SetFloat(AudioNames.MasterVolume, masterVolumePrefs);
        // }

        // masterVolumeSlider.value = masterVolumePrefs == -1f ? DefaultVolume : masterVolumePrefs;
        SetVolumeFromPrefs(AudioNames.MasterVolume, masterVolumeSlider);
    }

    private void SetVolumeFromPrefs(string audioName, Slider slider)
    {
        float volumePrefs = PlayerPrefs.GetFloat(audioName, DefaultVolume);
        mainAudioMixer.SetFloat(audioName, volumePrefs);

        slider.value = volumePrefs == -1f ? DefaultVolume : volumePrefs;
    }

    public void SetMasterVolume(float volume)
    {
        SaveVolumeSettings(AudioNames.MasterVolume, volume);
    }

    public void SetMusicVolume(float volume)
    {
        SaveVolumeSettings(AudioNames.MusicVolume, volume);
    }

    public void SetSFXVolume(float volume)
    {
        SaveVolumeSettings(AudioNames.SFXVolume, volume);
    }

    public void SaveVolumeSettings(string volumeName, float volume)
    {
        volume = LinearToDecibel(volume);
        mainAudioMixer.SetFloat(volumeName, volume);
        PlayerPrefs.SetFloat(volumeName, volume);
        PlayerPrefs.Save();
        Debug.Log("Saved " + volumeName + " as " + volume);
    }

    // Source
    // https://discussions.unity.com/t/how-to-convert-decibel-db-number-to-audio-source-volume-number-0to1/46543/4
    private float LinearToDecibel(float linear, float minDecibelVal = -80.0f)
    {
        return linear != 0 ? 20.0f * Mathf.Log10(linear) : minDecibelVal;
    }
}
