using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour
{
    [SerializeField] private float dashPower = 5.0f;
    [SerializeField] private float dashCooldown = 2.0f;
    [SerializeField] private float dashDuration = 0.25f;

    private Rigidbody2D rb;
    private bool canDash = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public IEnumerator PerformDash(Vector2 direction)
    {
        if (!canDash) yield break;

        canDash = false;
        Vector2 dashVelocity = direction.normalized * dashPower;
        rb.velocity = dashVelocity;

        yield return new WaitForSeconds(dashDuration);
        rb.velocity = Vector2.zero; // Stop dashing

        yield return new WaitForSeconds(dashCooldown - dashDuration);
        canDash = true;
    }
}
