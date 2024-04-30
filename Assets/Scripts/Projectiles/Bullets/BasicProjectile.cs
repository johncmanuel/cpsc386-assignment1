using System.Collections;
using UnityEngine;

public class BasicProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] ProjectileConfig projectileConfig;
    ProjectileType IProjectile.Type => projectileConfig.type;
    private ProjectileManager projectileManager;
    private ICollisionBehavior collisionBehavior;
    private Coroutine lifetimeCoroutine;

    void OnEnable()
    {
        projectileManager = ProjectileManager.Instance;
        if (projectileManager == null) Debug.LogError("Couldn't find required ProjectileManager component");

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>() ?? GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
            spriteRenderer.sprite = projectileConfig.sprite;
        else Debug.LogError("Could not find SpriteRenderer on Object or Child Object");

        collisionBehavior = new DamageOnCollision(projectileConfig.damage);

        // Reset and start the lifetime countdown every time the bullet is enabled
        lifetimeCoroutine = StartCoroutine(DestroyAfterTime(projectileConfig.lifetime));
    }

    public void OnHitDamageable(IDamageable hitDamageable, float? damageModifier)
    {
        if (hitDamageable == null)
        {
            Debug.Log("hitDamageable component is null in OnHitDamageable");
            return;
        }

        float damageScalar = (damageModifier == null) ? 1f : damageModifier.Value;

        hitDamageable.TakeDamage(projectileConfig.damage * damageScalar);
        DestroyProjectile();
    }

    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        DestroyProjectile();
    }

    public void DestroyProjectile()
    {
        StopCoroutine(lifetimeCoroutine);
        projectileManager.ReturnProjectileToPool(gameObject);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        collisionBehavior.ApplyCollisionEffect(other.gameObject);
        DestroyProjectile();
    }
}