using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

// Created once the player enters the first level of the game:
// Dungeon1
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    // Track enemies' status throughout the game. If they're alive, set status to true.
    // else, set it to false
    private Dictionary<string, bool> _enemyStatuses = new Dictionary<string, bool>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Destroying duplicate EnemyManager instance...");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        SetupEnemyStatuses();
    }

    private void SetupEnemyStatuses()
    {
        // Store enemies data once the game starts in the first level
        Debug.Log("Setting up enemy statuses...");

        var enemies = FindObjectsOfType<BaseEnemy>();
        foreach (var enemy in enemies)
        {
            // TODO: Check saved data to see if enemy is dead
            // ...

            SetEnemyStatus(enemy.name, true);

            var enemyRoom = enemy.transform.parent.gameObject;

            // Deactivate enemies in other rooms other than the first room the 
            // player starts in
            if (enemyRoom.name != "Room1")
            {
                GameManager.Instance.AddInactiveGameObject(enemy.name, enemy.gameObject);
                enemy.gameObject.SetActive(false);
            }
        }

        Debug.Log("Enemy count: " + _enemyStatuses.Count);

        if (_enemyStatuses.Count == 0)
        {
            Debug.LogError("No enemies found in the scene.");
        }
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
        foreach (var inactiveObj in GameManager.Instance.GetInactiveGameObjs())
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
            Debug.Log("Enemy count: " + _enemyStatuses.Count);

            _enemyStatuses = _enemyStatuses.ToDictionary(entry => entry.Key, entry => true);

            // UpdateGameState(GameStateType.LevelCompleted);
            GameManager.Instance.SwitchToScene("VictoryMenu");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // TODO: Update enemy count based on new scenes
    }

}
