using UnityEngine;

public interface IProjectile
{
    void DetectCollision(GameObject target);
    void OnHitTarget(GameObject hitObject);
    void DestroyProjectile();
}
