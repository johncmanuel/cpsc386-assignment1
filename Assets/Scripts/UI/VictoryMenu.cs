using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryMenu : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;

        if (gameManager == null)
        {
            Debug.LogError("Game Manager is null");
            return;
        }
    }

    // TODO: refactor these methods to be reusuable by other menus
    public void PlayAgain()
    {
        Debug.Log("Playing again");

        // Go back to the first level of the game
        gameManager.SwitchToScene("Dungeon1");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        gameManager.QuitGame();
    }
}
