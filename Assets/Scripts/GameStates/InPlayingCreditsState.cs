using UnityEngine;

internal class InPlayingCreditsState : IGameState
{
    public void OnEnter(GameManager manager)
    {
        Debug.Log("Playing Credits State");
    }

    public void OnExit(GameManager manager)
    {
        Debug.Log("Exiting Credits State");
        manager.TriggerSceneTransition();

    }
}