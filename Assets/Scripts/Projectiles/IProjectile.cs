using UnityEngine;

public interface IProjectile
{
    string Type { get; }
    void OnHitDamageable(IDamageable damageableComponent, float? damageModifier);
    void DestroyProjectile();
}
