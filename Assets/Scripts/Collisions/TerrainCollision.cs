using UnityEngine;

// Mainly used to destroy projectiles when they collide with terrain 
public class TerrainCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(Tags.Projectile)) return;

        if (!collision.gameObject.TryGetComponent<IProjectile>(out var projectile))
        {
            Debug.LogError("Could not find IProjectile component");
            return;
        }

        projectile.DestroyProjectile();
    }
}
