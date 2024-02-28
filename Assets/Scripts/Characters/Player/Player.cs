using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Invulnerability))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    private Rigidbody2D rb;

    private Invulnerability invulnerabilityComponent;

    public float Health
    {
        get => health;
        set => health = Mathf.Max(value, 0);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        invulnerabilityComponent = GetComponent<Invulnerability>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10);
        }
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
