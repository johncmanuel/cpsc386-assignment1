using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyBtns : MonoBehaviour
{
    public void SetDifficulty(string difficulty)
    {
        // Set the enemy difficulty based on the player's choice
        GameManager.Instance.enemyDifficulty = difficulty;
        GameManager.Instance.SwitchToScene("Dungeon1");
    }

    public void SetEasy()
    {
        SetDifficulty("Easy");
    }

    public void SetNormal()
    {
        SetDifficulty("Normal");
    }

    public void SetHard()
    {
        SetDifficulty("Hard");
    }
}
