using UnityEngine;

public class BasicMovement : MonoBehaviour, IMovementContribution
{
    public Vector2 currentDirection = Vector2.zero;
    public float speed = 5f;

    [SerializeField] private MovementManager movementManager;

    void Start()
    {
        SetUpMovementManager();
    }

    void SetUpMovementManager()
    {
        // Try to use the serialized field first, then look for the component on the current GameObject or an ancestor
        if (movementManager == null)
        {
            movementManager = GetComponent<MovementManager>() ?? GetComponentInParent<MovementManager>();
        }

        // Log an error if the component is still not found after the search
        if (movementManager == null)
        {
            Debug.LogError("MovementManager is null. It was not assigned in the inspector, and could not be found in GameObject or Parent.");
        }
        else
        {
            movementManager.RegisterContributor(this);
        }
    }

    void OnDestroy()
    {
        movementManager.UnregisterContributor(this);
    }

    public Vector2 GetContribution()
    {
        return currentDirection.normalized * speed;
    }

    public void UpdateDirection(Vector2 newDirection)
    {
        currentDirection = newDirection;
    }
}