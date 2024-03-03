using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private string[] collisionCheckTags = { "Projectile" };

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(gameObject.name + "'s hitbox collided with: " + collision.gameObject.name);

        // check for collision with desired collision tags
        foreach (var tag in collisionCheckTags)
        {
            if (!collision.gameObject.CompareTag(tag)) continue;

            if (collision.gameObject.CompareTag("Projectile"))
            {
                var projectile = collision.gameObject.GetComponent<IProjectile>();

                if (projectile == null) continue;

                projectile.OnHitTarget(gameObject);
            }
        }
    }
}