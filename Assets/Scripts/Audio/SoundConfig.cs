using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "SoundConfig", menuName = "Audio/SoundConfig")]
public class SoundConfig : ScriptableObject
{
    public string soundName;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 0.7f;
    [Range(.1f, 3f)] public float pitch = 1f;
    public bool loop = false;
    public AudioMixerGroup mixerGroup;
}
