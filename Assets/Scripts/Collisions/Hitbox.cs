using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private string[] collisionCheckTags = { Tags.Projectile };
    [SerializeField] private float damageScalar = 1f;
    private IDamageable damageableComponent;

    private void Start()
    {
        // Search for the IDamageable component up the hierarchy
        if (damageableComponent == null)
        {
            damageableComponent = GetComponent<IDamageable>() ?? GetComponentInParent<IDamageable>();
        }

        if (damageableComponent == null)
        {
            Debug.LogError("No IDamageable component found in GameObject or Parent.");
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var tag in collisionCheckTags)
        {
            if (!collision.gameObject.CompareTag(tag)) continue;
            var projectile = collision.gameObject.GetComponent<IProjectile>();
            if (projectile == null)
            {
                Debug.LogError("Could not find IProjectile component");
                continue;
            }

            // Pass the damage handling to the parent IDamageable
            projectile.OnHitDamageable(damageableComponent, damageScalar);
        }
    }
}
