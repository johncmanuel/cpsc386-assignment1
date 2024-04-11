using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Invulnerability))]
public class DashMovement : MonoBehaviour, IMovementContribution
{
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashStrength = 20f;
    [SerializeField] private float dashCooldownDuration = 0.2f;

    private bool isDashing = false;
    private bool isOnCooldown = false;

    private Vector2 dashDirection = Vector2.zero;

    [SerializeField] private Invulnerability invulnerability;
    [SerializeField] private MovementManager movementManager;


    void Start()
    {
        SetUpMovementManager();

        if (invulnerability == null)
            invulnerability = GetComponent<Invulnerability>() ?? GetComponentInChildren<Invulnerability>();

        if (invulnerability == null)
            Debug.LogError("Could not find required Invulnerability component");
    }

    void SetUpMovementManager()
    {
        if (movementManager == null)
        {
            movementManager = GetComponent<MovementManager>() ?? GetComponentInParent<MovementManager>();
        }

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
        if (isOnCooldown)
            return;

        if (!isDashing)
        {
            dashDirection = direction;
            invulnerability.BecomeInvulnerable();
            StartCoroutine(DashDuration());
        }
    }

    private IEnumerator DashDuration()
    {
        isDashing = true;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        isOnCooldown = true;
        yield return new WaitForSeconds(dashCooldownDuration);
        isOnCooldown = false;
    }
}
