using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Invulnerability))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    private float maxHealth;
    private GameManager gameManager;

    private Rigidbody2D rb;

    private Invulnerability invulnerabilityComponent;

    [SerializeField] private HealthBar healthBar;

    public float Health
    {
        get => health;
        set => health = Mathf.Max(value, 0);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        invulnerabilityComponent = GetComponent<Invulnerability>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        maxHealth = Health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Projectile))
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

        healthBar.UpdateHealthBar(Health / maxHealth);
    }

    public void Die()
    {
        Debug.Log("Player died!!");

        // Switch to the game over menu if the player dies
        gameManager.UpdateGameState(GameStateType.PlayerDied);
        gameManager.SwitchToScene("GameOverMenu");
    }

    public void Attack()
    {
        throw new System.NotImplementedException();
    }
}
