using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer mainAudioMixer;
    [SerializeField] private Slider masterVolumeSlider;
    private const float DefaultVolume = -15f;

    // Start off as active, then deactivate after loading volume values from Prefs
    private void Start()
    {
        if (mainAudioMixer == null)
            Debug.LogError("Main AudioMixer not set in the inspector");

        if (masterVolumeSlider == null)
            Debug.LogError("Master volume slider not found");

        InitializeVolume();

        gameObject.SetActive(false);
    }

    private void InitializeVolume()
    {
        float masterVolumePrefs = PlayerPrefs.GetFloat(AudioNames.MasterVolume);
        if (masterVolumePrefs == -1f)
        {
            Debug.Log("Master volume not found in prefs, setting to default");
            mainAudioMixer.SetFloat(AudioNames.MasterVolume, DefaultVolume);
        }
        else
        {
            Debug.Log("Master volume found in prefs, setting to " + masterVolumePrefs);
            mainAudioMixer.SetFloat(AudioNames.MasterVolume, masterVolumePrefs);
        }

        masterVolumeSlider.value = masterVolumePrefs == -1f ? DefaultVolume : masterVolumePrefs;
    }

    public void SetMasterVolume(float volume)
    {
        mainAudioMixer.SetFloat(AudioNames.MasterVolume, volume);
        PlayerPrefs.SetFloat(AudioNames.MasterVolume, volume);
        PlayerPrefs.Save();
    }
}
