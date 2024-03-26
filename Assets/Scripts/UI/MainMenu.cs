using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;

        if (gameManager == null)
        {
            Debug.LogError("Game Manager is null");
            return;
        }
    }

    public void PlayGame()
    {
        Debug.Log("Playing game");
        // gameManager.UpdateGameState(GameStateType.PlayingLevel);
        gameManager.SwitchToScene(gameManager.GetActiveSceneBuildIndex() + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");

        // Handle anything here before the game is exited.
        // ...

        gameManager.QuitGame();
    }
}
