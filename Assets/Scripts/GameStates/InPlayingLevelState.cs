using UnityEngine;

internal class InPlayingLevelState : IGameState
{
    public void OnEnter(GameManager manager)
    {
        if (AudioManager.Instance.isPlaying(VolumeNames.PlayingLevelMusicName)) return;
        Debug.Log("Playing level music");
        AudioManager.Instance.Play(VolumeNames.PlayingLevelMusicName);
    }

    public void OnExit(GameManager manager)
    {
        Debug.Log("Stopping level music");
        AudioManager.Instance.FadeOut(VolumeNames.PlayingLevelMusicName, 0.5f);
    }
}