using UnityEngine;

internal class InMainMenuState : IGameState
{
    public void OnEnter(GameManager manager)
    {
        if (AudioManager.Instance.isPlaying(AudioNames.MainMenuMusicName)) return;
        Debug.Log("Playing main menu music");
        AudioManager.Instance.Play(AudioNames.MainMenuMusicName);

        PlayerData.ResetPlayerData();
    }

    public void OnExit(GameManager manager)
    {
        Debug.Log("Stopping main menu music");
        AudioManager.Instance.FadeOut(AudioNames.MainMenuMusicName, 0.5f);
        manager.TriggerSceneTransition();
    }
}