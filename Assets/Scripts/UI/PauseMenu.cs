using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void ResumeGame()
    {
        // Set the pause menu to inactive
        GameManager.Instance.ResumeTime();
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        GameManager.Instance.ResumeTime();
        GameManager.Instance.QuitGame();
    }

    public void GoToMainMenu()
    {
        GameManager.Instance.ResumeTime();
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
