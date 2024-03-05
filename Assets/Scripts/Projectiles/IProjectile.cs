using UnityEngine;

public interface IProjectile
{
    string Type { get; }
    void OnHitTarget(GameObject hitObject);
    void DestroyProjectile();
}
