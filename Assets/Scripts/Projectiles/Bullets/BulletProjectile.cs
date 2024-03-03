using System.Collections;
using UnityEngine;

public class BulletProjectile : MonoBehaviour, IProjectile
{
    [SerializeField]
    private float damageAmount = 10f;
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float lifetime = 4f;
    private Rigidbody2D rb;
    private GameObject parentObj;
    private ProjectileManager projectileManager;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        parentObj = transform.parent.gameObject;
        projectileManager = GetComponentInParent<ProjectileManager>();

        // Destroy the bullet projectile after a certain amount of time
        StartCoroutine(DestroyAfterTime(lifetime));

        // Shoot the bullet projectile in the direction of the player's cursor
        if (parentObj.CompareTag("Player"))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseDir = mousePos - transform.position;
            rb.velocity = new Vector2(mouseDir.x, mouseDir.y).normalized * speed;
        }
        // Shoot the bullet projectile in the player's direction
        else if (parentObj.CompareTag("Enemy"))
        {
            Transform target = GameObject.FindGameObjectWithTag("Player").transform;
            Vector2 direction = target.position - transform.position;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        }
    }

    private void Update()
    {

    }

    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        DestroyProjectile();
    }

    public void OnHitTarget(GameObject hitObject)
    {
        Debug.Log("Bullet hit " + hitObject.name);

        if (hitObject.CompareTag("Player"))
        {
            // deal damage to the player if the bullet was shot by an enemy
            if (parentObj.CompareTag("Enemy"))
                hitObject.GetComponent<Player>().TakeDamage(damageAmount);
        }
        else if (hitObject.CompareTag("Enemy"))
        {
            // deal damage to the enemy if the bullet was shot by the player
            if (parentObj.CompareTag("Player"))
                hitObject.GetComponent<BaseEnemy>().TakeDamage(damageAmount);
        }

        DestroyProjectile();
    }

    public void DestroyProjectile()
    {
        projectileManager.DeleteProjectile(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log("Bullet destroyed");
    }

    public void DetectCollision(GameObject target)
    {
        // OnHitTarget(target);
        // DestroyProjectile();
        throw new System.NotImplementedException();
    }
}
