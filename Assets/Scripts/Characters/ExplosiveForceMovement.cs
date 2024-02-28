using UnityEngine;

public class ExplosiveForceMovement : MonoBehaviour, IMovementContribution
{
    public Vector2 forceDirection = Vector2.zero;
    public float forceMagnitude = 0f;

    private MovementManager movementManager;
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
        Vector2 contribution = forceDirection.normalized * forceMagnitude;
        return contribution;
    }

    public void ApplyForce(Vector2 direction, float magnitude)
    {
        forceDirection = direction;
        forceMagnitude = magnitude;
    }
}
