using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGameStateOnLoad : MonoBehaviour
{
    [SerializeField] private GameStateType newGameStateType = GameStateType.Null;

    void Start()
    {
        if (newGameStateType == GameStateType.Null)
            Debug.LogError("newGameStateType is GameStateType.Null, you probably need to set this in the inspector");

        if (GameManager.Instance == null)
            Debug.LogError("GameManager Singleton Instance is null, ensure it exists in the scene.");

        GameManager.Instance.UpdateGameState(newGameStateType);
    }
}
