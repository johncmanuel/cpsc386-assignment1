using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.AddInactiveGameObject("PauseMenu", gameObject);
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
        GameManager.Instance.SwitchToScene("MainMenu");
    }

    public void SaveGame()
    {
        // Save the game
    }
}
