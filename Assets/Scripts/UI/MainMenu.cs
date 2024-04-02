using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Playing game");
        // gameManager.UpdateGameState(GameStateType.PlayingLevel);
        GameManager.Instance.SwitchToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");

        // Handle anything here before the game is exited.
        // ...

        GameManager.Instance.QuitGame();
    }
}
