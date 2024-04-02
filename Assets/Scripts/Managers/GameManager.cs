using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public interface IGameState
{
    void OnEnter(GameManager manager);
    void OnExit(GameManager manager);
}

public enum GameStateType
{
    Null,
    InMainMenu,
    PreparingLevel,
    PlayingLevel,
    LevelPaused,
    PlayerDied,
    LevelCompleted,
    PlayingCredits
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameStateType CurrentStateType { get; private set; }
    private IGameState _currentState;
    private int _currentSceneNum = 0;
    private int _totalNumberOfScenes;

    // Track necessary game objects that are not currently active, such as
    // UI menus.
    private Dictionary<string, GameObject> _inactiveGameObjects = new Dictionary<string, GameObject>();

    // Track enemies' status throughout the game
    private Dictionary<string, bool> _enemyStatuses = new Dictionary<string, bool>();

    public event Action<GameStateType> OnGameStateChange;

    private readonly Dictionary<GameStateType, IGameState> _gameStates = new Dictionary<GameStateType, IGameState>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _totalNumberOfScenes = SceneManager.sceneCountInBuildSettings;

        Instance = this;
        DontDestroyOnLoad(gameObject);
        QualitySettings.vSyncCount = 0;
        InitializeGameStates();

        // Store enemies data
        var enemies = FindObjectsOfType<BaseEnemy>();
        foreach (var enemy in enemies)
        {
            // TODO: Check saved data to see if enemy is dead
            // ...

            SetEnemyStatus(enemy.name, true);

            var enemyRoom = enemy.transform.parent.gameObject;
            if (enemyRoom.name != "Room1")
            {
                AddInactiveGameObject(enemy.name, enemy.gameObject);
                enemy.gameObject.SetActive(false);
            }
        }
    }

    private void InitializeGameStates()
    {
        _gameStates.Add(GameStateType.InMainMenu, new InMainMenuState());
        _gameStates.Add(GameStateType.PreparingLevel, new InPreparingLevelState());
        _gameStates.Add(GameStateType.PlayingLevel, new InPlayingLevelState());
        _gameStates.Add(GameStateType.LevelPaused, new InLevelPausedState());
        _gameStates.Add(GameStateType.PlayerDied, new InPlayerDiedState());
        _gameStates.Add(GameStateType.LevelCompleted, new InLevelCompletedState());
        _gameStates.Add(GameStateType.PlayingCredits, new InPlayingCreditsState());
    }

    public void UpdateGameState(GameStateType newState)
    {
        if (newState == GameStateType.Null)
        {
            Debug.LogWarning("GameStateType {newState} is GameStateType.Null");
            return;
        }

        if (!_gameStates.ContainsKey(newState))
        {
            Debug.LogError("GameStateType {newState} does not exist in _gameStates. Check InitializeGameStates() contains the required state mapping.");
            return;
        }

        // Dont make change if one isnt necessary..
        if (CurrentStateType == newState) return;

        _currentState?.OnExit(this);
        OnGameStateChange?.Invoke(newState);

        CurrentStateType = newState;
        _currentState = _gameStates[newState];
        _currentState?.OnEnter(this);
    }

    public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void SwitchToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SwitchToScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void PauseGame()
    {
        // Bring up the pause menu
        // UpdateGameState(GameStateType.LevelPaused);
        GameObject pauseMenu = GetInactiveGameObject("PauseMenu");

        // I wonder if there's a one liner for this...
        if (pauseMenu != null && pauseMenu.activeSelf == false)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AddInactiveGameObject(string key, GameObject gameObject)
    {
        if (_inactiveGameObjects.ContainsKey(key))
        {
            Debug.LogWarning($"Key {key} already exists in _inactiveGameObjects. Will overwrite current object.");
            _inactiveGameObjects[key] = gameObject;
        }
        else
        {
            _inactiveGameObjects.Add(key, gameObject);
        }
    }

    public GameObject GetInactiveGameObject(string key)
    {
        if (_inactiveGameObjects.ContainsKey(key))
        {
            return _inactiveGameObjects[key];
        }

        Debug.LogError($"Key {key} does not exist in _inactiveGameObjects.");
        return null;
    }

    public void SetEnemyStatus(string key, bool status)
    {
        if (_enemyStatuses.ContainsKey(key))
        {
            _enemyStatuses[key] = status;
        }
        else
        {
            _enemyStatuses.Add(key, status);
        }
    }

    public bool GetEnemyStatus(string key)
    {
        if (_enemyStatuses.ContainsKey(key))
        {
            return _enemyStatuses[key];
        }

        Debug.LogError($"Key {key} does not exist in _enemyStatuses.");
        return false;
    }

    public void SpawnEnemies(string roomName)
    {
        // Spawn enemies in the room
        foreach (var inactiveObj in _inactiveGameObjects)
        {
            if (!inactiveObj.Value.CompareTag(Tags.Enemy))
                continue;
            if (inactiveObj.Value.transform.parent.gameObject.name != roomName)
                continue;
            inactiveObj.Value.SetActive(true);
        }
    }

    public void CheckAllEnemiesEliminated()
    {
        if (!_enemyStatuses.ContainsValue(true))
        {
            Debug.Log("All enemies are dead!");
            // UpdateGameState(GameStateType.LevelCompleted);
            SwitchToScene("VictoryMenu");
        }
    }
}