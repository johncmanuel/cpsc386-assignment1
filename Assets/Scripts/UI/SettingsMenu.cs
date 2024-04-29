using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer mainAudioMixer;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider SFXVolumeSlider;

    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private Toggle vsyncToggle;

    // Value in decibels
    private const float DefaultVolume = -15f;

    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private RefreshRate currentRefreshRate;
    private int currentResolutionIndex = 0;

    // Start off as active, then deactivate after loading volume values from Prefs 
    // and other values from Prefs
    private void Start()
    {
        if (mainAudioMixer == null)
            Debug.LogError("Main AudioMixer not set in the inspector");
        if (masterVolumeSlider == null)
            Debug.LogError("Master volume slider not found");
        if (musicVolumeSlider == null)
            Debug.LogError("Music volume slider not found");
        if (SFXVolumeSlider == null)
            Debug.LogError("SFX volume slider not found");
        if (fullScreenToggle == null)
            Debug.LogError("Fullscreen toggle not found");
        if (vsyncToggle == null)
            Debug.LogError("Vsync toggle not found");

        InitializeResolutions();

        InitializeVolumeFromPrefs();
        InitializeVisualSettingsFromPrefs();

        var settingsScrollView = GameObject.Find("Settings Scroll View");
        settingsScrollView.SetActive(false);
    }

    private void InitializeVolumeFromPrefs()
    {
        SetVolumeFromPrefs(PrefsKeys.MasterVolume, masterVolumeSlider);
        SetVolumeFromPrefs(PrefsKeys.MusicVolume, musicVolumeSlider);
        SetVolumeFromPrefs(PrefsKeys.SFXVolume, SFXVolumeSlider);
    }

    private void SetVolumeFromPrefs(string audioName, Slider slider)
    {
        float volumePrefs = PlayerPrefs.GetFloat(audioName, DefaultVolume);
        mainAudioMixer.SetFloat(audioName, volumePrefs);

        // Sliders use linear values
        // TODO: Maybe store 2 values in PlayerPrefs, one for linear and one for decibel?
        // This would be better for accurate results
        slider.value = DecibelToLinear(volumePrefs);
    }

    public void SetMasterVolume(float volume)
    {
        SaveVolumeSettings(PrefsKeys.MasterVolume, volume);
    }

    public void SetMusicVolume(float volume)
    {
        SaveVolumeSettings(PrefsKeys.MusicVolume, volume);
    }

    public void SetSFXVolume(float volume)
    {
        SaveVolumeSettings(PrefsKeys.SFXVolume, volume);
    }

    public void SaveVolumeSettings(string volumeName, float volume)
    {
        // Assumes that volume is obtained from a slider
        volume = LinearToDecibel(volume);

        mainAudioMixer.SetFloat(volumeName, volume);
        PlayerPrefs.SetFloat(volumeName, volume);
        PlayerPrefs.Save();
        Debug.Log("Saved " + volumeName + " as " + volume);
    }

    // Source for conversion methods:
    // https://discussions.unity.com/t/how-to-convert-decibel-db-number-to-audio-source-volume-number-0to1/46543/4
    private float LinearToDecibel(float linear, float minDecibelVal = -80.0f)
    {
        return linear != 0 ? 20.0f * Mathf.Log10(linear) : minDecibelVal;
    }

    private float DecibelToLinear(float decibel)
    {
        return Mathf.Pow(10.0f, decibel / 20.0f);
    }

    private void InitializeVisualSettingsFromPrefs()
    {
        SetFullScreenFromPrefs();
        SetVsyncFromPrefs();
        SetResolutionFromPrefs();
    }

    // Source:
    // https://www.youtube.com/watch?v=HnvPNoU9Wjw
    private void InitializeResolutions()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRateRatio;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRateRatio.value != currentRefreshRate.value) continue;
            filteredResolutions.Add(resolutions[i]);
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            // Sometimes, the refresh rate would be a crazy decimal like 60.000038472882 (on my end), 
            // so round it to the nearest whole number.
            double refreshRateWholeNum = Math.Round(filteredResolutions[i].refreshRateRatio.value, 0);
            string resolutionOption = filteredResolutions[i].width + " x " + filteredResolutions[i].height + " @ " +
                                      refreshRateWholeNum + "Hz";
            options.Add(resolutionOption);

            if (filteredResolutions[i].width == Screen.currentResolution.width &&
                filteredResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
        SaveResolutionPrefs(resolutionIndex);
    }

    private void SaveResolutionPrefs(int resolutionIndex)
    {
        PlayerPrefs.SetInt(PrefsKeys.ResolutionIndex, resolutionIndex);
        PlayerPrefs.Save();
        Debug.Log("Saved resolution index as " + resolutionIndex);
    }

    private void SetResolutionFromPrefs()
    {
        int resolutionIndex = PlayerPrefs.GetInt(PrefsKeys.ResolutionIndex, 0);
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
        resolutionDropdown.value = resolutionIndex;
        resolutionDropdown.RefreshShownValue();
        Debug.Log("Set resolution to " + resolution.width + " x " + resolution.height);
    }

    private void SetFullScreenFromPrefs()
    {
        bool isFullScreen = IntToBool(PlayerPrefs.GetInt(PrefsKeys.FullScreen, 1));
        Screen.fullScreenMode = isFullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        fullScreenToggle.isOn = isFullScreen;
        Debug.Log("Set fullscreen to " + isFullScreen);
    }

    private void SetVsyncFromPrefs()
    {
        bool isVsync = IntToBool(PlayerPrefs.GetInt(PrefsKeys.Vsync, 1));
        QualitySettings.vSyncCount = isVsync ? 1 : 0;
        vsyncToggle.isOn = isVsync;
        Debug.Log("Set vsync to " + isVsync);
    }

    public void ToggleFullScreen(bool isFullScreen)
    {
        Screen.fullScreenMode = isFullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        SaveBoolPrefs(PrefsKeys.FullScreen, isFullScreen);
    }

    public void ToggleVsync(bool isVsync)
    {
        QualitySettings.vSyncCount = isVsync ? 1 : 0;
        SaveBoolPrefs(PrefsKeys.Vsync, isVsync);
    }

    private void SaveBoolPrefs(string key, bool val)
    {
        PlayerPrefs.SetInt(key, BoolToInt(val));
        PlayerPrefs.Save();
        Debug.Log("Saved " + key + " as " + val);
    }

    private int BoolToInt(bool val)
    {
        return val ? 1 : 0;
    }

    private bool IntToBool(int val)
    {
        return val == 1;
    }
}
