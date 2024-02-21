using UnityEngine;

public class PlayerCharacter : MonoBehaviour, ICharacter, IAimable
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float moveSpeed = 5.0f;
    private IWeapon equippedWeapon;
    private bool isInvulnerable = false;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public float Health
    {
        get => health;
        set => health = Mathf.Max(value, 0);
    }

    public void Move(Vector2 direction)
    {
        Vector2 move = direction.normalized * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + move);
    }

    public void TakeDamage(float amount)
    {
        if (isInvulnerable) return;
        Health -= amount;
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Attack()
    {
        if (equippedWeapon != null)
        {
            equippedWeapon.Attack();
        }
    }

    public void AimTowards(Vector2 aimDirection)
    {
        // rotate character or weapon?
    }

    private void Die()
    {
        Debug.Log("Player died.");
    }

    public void SetInvulnerability(bool state, float duration)
    {
        isInvulnerable = state;
        if (state)
        {
            Invoke(nameof(ResetInvulnerability), duration);
        }
    }

    private void ResetInvulnerability()
    {
        isInvulnerable = false;
    }
}