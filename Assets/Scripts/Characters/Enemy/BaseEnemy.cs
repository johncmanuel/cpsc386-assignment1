using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseEnemy : MonoBehaviour, IDamageable, IMoveable, ICombatant
{
    [SerializeField] private float health;
    [SerializeField] private float moveSpeed = 3f;

    private Rigidbody2D rb;

    public float Health
    {
        get => health;
        set => health = Mathf.Max(value, 0);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public abstract void Attack();

    public virtual void Move(Vector2 direction)
    {
        Vector2 moveVelocity = direction.normalized * moveSpeed;
        rb.velocity = moveVelocity;
    }

    public virtual void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Die();
        }
        Debug.Log($"Enemy took {amount} damage. Remaining health: {Health}");
    }

    public virtual void Die()
    {
        Debug.Log("Enemy Died!");
        // Implement death logic here, like playing an animation or disabling the enemy
    }
}
