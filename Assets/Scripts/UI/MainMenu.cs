using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // gameManager.UpdateGameState(GameStateType.PlayingLevel);
        GameManager.Instance.SwitchToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        // Handle anything here before the game is exited.
        // ...

        GameManager.Instance.QuitGame();
    }
}
