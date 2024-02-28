using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Invulnerability))]
public class DashMovement : MonoBehaviour, IMovementContribution
{
    [SerializeField] private const float dashDuration = 0.2f;
    [SerializeField] private float dashStrength = 20f;
    
    private bool isDashing = false;
    private Vector2 dashDirection = Vector2.zero;


    [SerializeField] private Invulnerability invulnerabilityComponent;
    [SerializeField] private MovementManager movementManager;

    void Start()
    {
        SetUpMovementManager();

        if (invulnerabilityComponent == null)
            invulnerabilityComponent = GetComponent<Invulnerability>();

        if (invulnerabilityComponent == null)
            Debug.LogError("Could not find required Invulnerability component");
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
        if (isDashing)
        {
            return dashDirection.normalized * dashStrength;
        }
        return Vector2.zero;
    }

    public void PerformDash(Vector2 direction)
    {
        if (!isDashing)
        {
            dashDirection = direction;
            isDashing = true;
            invulnerabilityComponent.BecomeInvulnerable();
            StartCoroutine(DashDuration());
        }
    }

    private IEnumerator DashDuration()
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }
}
