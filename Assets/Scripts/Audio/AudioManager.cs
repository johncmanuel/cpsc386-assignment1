using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Collections;

public class AudioManager : MonoBehaviour, ISoundPlayer, ISoundAdjuster
{
    public static AudioManager Instance { get; private set; }
    public SoundConfig[] soundConfigs;
    private Dictionary<string, AudioSource> soundDictionary = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeSounds();
    }
    private void InitializeSounds()
    {
        foreach (SoundConfig config in soundConfigs)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = config.clip;
            source.volume = config.volume;
            source.pitch = config.pitch;
            source.loop = config.loop;
            source.outputAudioMixerGroup = config.mixerGroup;

            if (!soundDictionary.ContainsKey(config.soundName))
                soundDictionary.Add(config.soundName, source);
        }
    }

    public bool isPlaying(string soundName)
    {
        if (!soundDictionary.TryGetValue(soundName, out AudioSource source))
        {
            Debug.LogError($"Sound {soundName} not found");
            return false;
        }
        return source.isPlaying;
    }

    public void Play(string soundName)
    {
        if (!soundDictionary.TryGetValue(soundName, out AudioSource source))
        {
            Debug.LogError($"Sound {soundName} not found");
            return;
        }

        source.Play();
    }

    public void FadeOut(string soundName, float duration)
    {
        if (!soundDictionary.TryGetValue(soundName, out AudioSource source))
        {
            Debug.LogError($"Sound {soundName} not found");
            return;
        }

        StartCoroutine(FadeOutRoutine(source, duration));
    }

    private IEnumerator FadeOutRoutine(AudioSource source, float duration)
    {
        float startVolume = source.volume;

        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        source.Stop();
        source.volume = startVolume;
    }
}