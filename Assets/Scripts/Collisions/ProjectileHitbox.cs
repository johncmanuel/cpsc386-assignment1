using UnityEngine;

public class ProjectileHitbox : MonoBehaviour
{
    private string[] collisionCheckTags = { Tags.Projectile };

    void OnCollisionEnter2D(Collision2D collision)
    {
        // check for collision with desired collision tags
        foreach (var tag in collisionCheckTags)
        {
            if (!collision.gameObject.CompareTag(tag)) continue;

            // Always check for the Projectile tag
            var projectile = collision.gameObject.GetComponent<IProjectile>();

            if (projectile == null) continue;

            projectile.OnHitTarget(gameObject);
        }
    }
}