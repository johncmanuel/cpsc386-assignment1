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

    void Start()
    {
        projectileManager = ProjectileManager.Instance;
        if (projectileManager == null) Debug.LogError("Couldn't find required ProjectileManager component");

        collisionBehavior = new DamageOnCollision(damageAmount);

        StartCoroutine(DestroyAfterTime(lifetime));
    }

    public void OnHitTarget(GameObject hitObject)
    {
        Debug.Log("Bullet hit " + hitObject.name);
        hitObject.GetComponent<IDamageable>().TakeDamage(damageAmount);
        DestroyProjectile();
    }

    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        DestroyProjectile();
    }

    public void DestroyProjectile()
    {
        projectileManager.DeleteProjectile(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        collisionBehavior.ApplyCollisionEffect(other.gameObject);

        DestroyProjectile();
    }

    private void OnDestroy()
    {
        Debug.Log("Bullet destroyed");
    }
}
