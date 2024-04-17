using UnityEngine;

internal class InPlayerDiedState : IGameState
{
    public void OnEnter(GameManager manager)
    {
        Debug.Log("Entering Player Died State");
        PlayerData.ResetPlayerData();
        manager.SwitchToScene("GameOverMenu");
    }

    public void OnExit(GameManager manager)
    {
        Debug.Log("Exiting Player Died State");
        manager.TriggerSceneTransition();
    }
}