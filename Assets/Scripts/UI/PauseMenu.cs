using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
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

        gameManager.AddInactiveGameObject("PauseMenu", gameObject);
        gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        // Set the pause menu to inactive
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        // Go to the Main Menu
        gameManager.SwitchToScene("MainMenu");
    }

    public void SaveGame()
    {
        // Save the game
    }
}
