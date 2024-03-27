using UnityEngine;

public class EnterNextRoom : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]
    private string sceneName;

    private void Start()
    {
        gameManager = GameManager.Instance;

        if (gameManager == null)
        {
            Debug.LogError("GameManager Singleton instance not found. Ensure the GameManager exists in the scene by this point.");
        }

        if (sceneName == null)
        {
            Debug.LogError("Scene Name not set in inspector.");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(Tags.Player))
        {
            // GameManager.Instance.UpdateGameState(GameStateType.LevelCompleted);
            gameManager.SwitchToScene(sceneName);
        }
    }

}
