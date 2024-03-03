using UnityEngine;

public class EnterNextRoom : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]
    private string sceneName;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (gameManager == null)
            {
                Debug.LogError("Game Manager not set.");
                return;
            }

            if (sceneName == null)
            {
                Debug.LogError("Scene Name not set.");
                return;
            }

            gameManager.SwitchToScene(sceneName);
        }
    }

}
