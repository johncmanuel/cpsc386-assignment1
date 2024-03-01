using UnityEngine;

[RequireComponent(typeof(ProjectileManager))]
public class RangedEnemy : BaseEnemy
{
    private ProjectileManager projectileManager;

    private void Awake()
    {
        projectileManager = GetComponent<ProjectileManager>();
    }

    public override void Attack()
    {
        // Implement range-specific attack logic here
        Debug.Log("Ranged Enemy Attacks!");
        projectileManager.SpawnProjectile();
    }
}