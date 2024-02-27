using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IGameState
{
    void OnEnter(GameManager manager);
    void OnExit(GameManager manager);
}

public enum GameStateType
{
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
    private readonly int _totalNumberOfScenes = SceneManager.sceneCountInBuildSettings;

    public event Action<GameStateType> OnGameStateChange;

    private readonly Dictionary<GameStateType, IGameState> _gameStates = new Dictionary<GameStateType, IGameState>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        QualitySettings.vSyncCount = 0;
        InitializeGameStates();
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
        if (CurrentStateType == newState) return;

        _currentState?.OnExit(this);
        OnGameStateChange?.Invoke(newState);

        CurrentStateType = newState;
        _currentState = _gameStates[newState];
        _currentState?.OnEnter(this);
    }

    public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void SwitchToScene(string sceneName, bool updateCurrentSceneNum = true)
    {
        SceneManager.LoadScene(sceneName);
        if (updateCurrentSceneNum) UpdateCurrentSceneNum(sceneName);
    }

    public void SwitchToScene(int sceneNum, bool updateCurrentSceneNum = true)
    {
        SceneManager.LoadScene(sceneNum);
        if (updateCurrentSceneNum) _currentSceneNum = sceneNum;
    }

    private void UpdateCurrentSceneNum(string sceneName)
    {
        _currentSceneNum = SceneManager.GetSceneByName(sceneName).buildIndex;
    }
}