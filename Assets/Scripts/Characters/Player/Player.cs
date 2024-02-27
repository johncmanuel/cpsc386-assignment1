using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Invulnerability))]
[RequireComponent(typeof(Dash))]
public class Player : MonoBehaviour, IMoveable, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;

    private Dash dashComponent;
    private Invulnerability invulnerabilityComponent;

    public float Health
    {
        get => health;
        set => health = Mathf.Max(value, 0);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dashComponent = GetComponent<Dash>();
        invulnerabilityComponent = GetComponent<Invulnerability>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10);
        }
    }

    public void Move(Vector2 direction)
    {
        Vector2 moveVelocity = direction.normalized * moveSpeed;
        rb.velocity = moveVelocity;
    }

    public void TakeDamage(float amount)
    {
        if (invulnerabilityComponent.IsInvulnerable) return;

        Health -= amount;
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player died!!");
        // whatever happens when the player dies
    }

    public void Attack()
    {
        throw new System.NotImplementedException();
    }
}
