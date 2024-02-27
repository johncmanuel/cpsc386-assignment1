using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Invulnerability))]
[RequireComponent(typeof(Rigidbody2D))]
public class Dash : MonoBehaviour
{
    [SerializeField] private float dashPower = 5.0f;
    [SerializeField] private float dashCooldown = 1.0f;
    [SerializeField] private float dashDuration = 0.25f;

    private Rigidbody2D rb;
    private Invulnerability invulnerability;
    private bool canDash = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        invulnerability = GetComponent<Invulnerability>();
    }

    public IEnumerator PerformDash(Vector2 direction)
    {
        if (!canDash) yield break;

        canDash = false;
        Vector2 dashVelocity = direction.normalized * dashPower;
        rb.velocity = dashVelocity;

        invulnerability.BecomeInvulnerable();

        yield return new WaitForSeconds(dashDuration);
        rb.velocity = Vector2.zero; // Stop dashing

        yield return new WaitForSeconds(dashCooldown - dashDuration);
        canDash = true;
    }
}
