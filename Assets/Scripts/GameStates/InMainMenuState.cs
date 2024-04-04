using UnityEngine;

internal class InMainMenuState : IGameState
{
    public void OnEnter(GameManager manager)
    {
        if (AudioManager.Instance.isPlaying(VolumeNames.MainMenuMusicName)) return;
        Debug.Log("Playing main menu music");
        AudioManager.Instance.Play(VolumeNames.MainMenuMusicName);
    }

    public void OnExit(GameManager manager)
    {
        Debug.Log("Stopping main menu music");
        AudioManager.Instance.FadeOut(VolumeNames.MainMenuMusicName, 0.5f);
    }
}