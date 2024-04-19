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

    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private Toggle vsyncToggle;

    // Value in decibels
    private const float DefaultVolume = -15f;

    // Start off as active, then deactivate after loading volume values from Prefs
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


        InitializeVolumeFromPrefs();
        InitializeVisualSettings();

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

    private void InitializeVisualSettings()
    {
        SetFullScreenFromPrefs();
        SetVsyncFromPrefs();
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
