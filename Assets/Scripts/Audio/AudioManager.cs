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

    public void Play(string soundName)
    {
        if (soundDictionary.TryGetValue(soundName, out AudioSource source))
        {
            source.Play();
        }
        else
        {
            Debug.LogError($"Sound {soundName} not found");
        }
    }

    public void FadeOut(string soundName, float duration)
    {
        if (soundDictionary.TryGetValue(soundName, out AudioSource source))
        {
            StartCoroutine(FadeOutRoutine(source, duration));
        }
        else
        {
            Debug.LogError($"Sound {soundName} not found");
        }
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