using System;
using System.Collections.Generic;
using System.Linq;
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

    public event Action<GameStateType> OnGameStateChange;

    private readonly Dictionary<GameStateType, IGameState> _gameStates = new Dictionary<GameStateType, IGameState>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Destroying duplicate GameManager instance...");
            Destroy(gameObject);
            return;
        }

        _totalNumberOfScenes = SceneManager.sceneCountInBuildSettings;

        Instance = this;
        DontDestroyOnLoad(gameObject);
        QualitySettings.vSyncCount = 0;
        InitializeGameStates();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
        // UpdateGameState(GameStateType.LevelPaused);

        // Bring up the pause menu
        var pauseMenu = FindObjectOfType<PauseMenu>(includeInactive: true).gameObject;

        if (pauseMenu != null && pauseMenu.activeSelf == false)
        {
            Time.timeScale = 1f - Time.timeScale;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
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

    public Dictionary<string, GameObject> GetInactiveGameObjs()
    {
        return _inactiveGameObjects;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string[] menuSceneNames = { "VictoryMenu", "GameOverMenu", "MainMenu" };
        if (menuSceneNames.Contains(scene.name))
        {
            // Reset player data
            PlayerData.PlayerHealth = 0;
            PlayerData.PlayerGun = null;
            PlayerData.PlayerGunObj = null;
            PlayerData.PlayerPosition = Vector3.zero;
        }
    }
}