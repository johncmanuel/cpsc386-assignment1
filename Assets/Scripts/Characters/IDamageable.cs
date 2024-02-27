using UnityEngine;

public interface IDamageable
{
    public float Health { get; set; }
    void TakeDamage(float amount);
    void Die();
}