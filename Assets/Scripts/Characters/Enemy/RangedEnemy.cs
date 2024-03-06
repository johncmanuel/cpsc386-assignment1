using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    private ProjectileManager projectileManager;
    private float bulletSpeed = 1f;

    private new void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;

        if (projectileManager == null)
            Debug.LogError("Could not find required component ProjectileManager");
    }

    public override void Attack()
    {
        // Implement range-specific attack logic here
        Debug.Log("Ranged Enemy Attacks!");
        GameObject bullet = projectileManager.SpawnProjectile("Bullet");

        // Spawn it on the enemy location
        bullet.transform.SetLocalPositionAndRotation(transform.position, Quaternion.identity);

        // Make it shoot to the player
        Transform target = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        Vector2 direction = target.position - transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;
    }
}