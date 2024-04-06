using UnityEngine;

internal class InPlayingLevelState : IGameState
{
    public void OnEnter(GameManager manager)
    {
        if (AudioManager.Instance.isPlaying(AudioNames.PlayingLevelMusicName)) return;
        Debug.Log("Playing level music");
        AudioManager.Instance.Play(AudioNames.PlayingLevelMusicName);
    }

    public void OnExit(GameManager manager)
    {
        Debug.Log("Stopping level music");
        AudioManager.Instance.FadeOut(AudioNames.PlayingLevelMusicName, 0.5f);

        manager.TriggerSceneTransition();
    }
}