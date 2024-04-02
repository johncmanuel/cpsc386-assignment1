using UnityEngine;

public class EnterNextRoom : MonoBehaviour
{
    [SerializeField] private Transform targetPosition;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(Tags.Player))
        {
            Debug.Log("Player entered a room");
            // GameManager.Instance.UpdateGameState(GameStateType.LevelCompleted);

            col.gameObject.transform.position = targetPosition.position;
            Camera.main.transform.position = col.gameObject.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(Tags.Player))
        {
            Debug.Log("Player exited a room");
        }
    }
}
