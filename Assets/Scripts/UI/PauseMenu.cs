using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void ResumeGame()
    {
        // Set the pause menu to inactive
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        // Go to the Main Menu
        GameManager.Instance.SwitchToScene("MainMenu");
    }

    public void SaveGame()
    {
        // Save the game
    }

    public void Settings()
    {
        // Go to the settings menu
    }
}
