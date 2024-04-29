using System.Collections;
using UnityEngine;

public class BulletProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float lifetime = 10f;

    private ProjectileManager projectileManager;

    private string projectileType = "Bullet";
    public string Type => projectileType;

    private ICollisionBehavior collisionBehavior;

    private Coroutine lifetimeCoroutine;

    void OnEnable()
    {
        projectileManager = ProjectileManager.Instance;
        if (projectileManager == null) Debug.LogError("Couldn't find required ProjectileManager component");

        collisionBehavior = new DamageOnCollision(damageAmount);

        // Reset and start the lifetime countdown every time the bullet is enabled
        lifetimeCoroutine = StartCoroutine(DestroyAfterTime(lifetime));
    }

    public void OnHitDamageable(IDamageable hitDamageable, float? damageModifier)
    {
        if (hitDamageable == null)
        {
            Debug.Log("hitDamageable component is null in OnHitDamageable");
            return;
        }

        float damageScalar = (damageModifier == null) ? 1f : damageModifier.Value;

        hitDamageable.TakeDamage(damageAmount * damageScalar);
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