using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryMenu : MonoBehaviour
{
    public void PlayAgain()
    {
        // Go back to the first level of the game
        GameManager.Instance.SwitchToScene("Dungeon1");
    }

    public void QuitGame()
    {
        GameManager.Instance.SwitchToScene("MainMenu");
    }
}
