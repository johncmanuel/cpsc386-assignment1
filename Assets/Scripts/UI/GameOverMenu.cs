using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public void PlayAgain()
    {
        GameManager.Instance.SwitchToScene("Dungeon1");
    }

    public void QuitGame()
    {
        GameManager.Instance.SwitchToScene("MainMenu");
    }
}
