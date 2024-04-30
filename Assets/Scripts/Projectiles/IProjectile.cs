using UnityEngine;

public interface IProjectile
{
    ProjectileType Type { get; }
    void OnHitDamageable(IDamageable damageableComponent, float? damageModifier);
    void DestroyProjectile();
}
