using UnityEngine;

public interface ICollisionBehavior
{
    void ApplyCollisionEffect(GameObject other);
}

public class DamageOnCollision : ICollisionBehavior
{
    private float damageAmount;

    public DamageOnCollision(float damage)
    {
        damageAmount = damage;
    }

    public void ApplyCollisionEffect(GameObject other)
    {
        var damageable = other.GetComponent<IDamageable>();
        damageable?.TakeDamage(damageAmount);
    }
}