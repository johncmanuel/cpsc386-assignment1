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

    private string projectileType = "BulletProjectile";
    public string Type => projectileType;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        parentObj = transform.parent.gameObject;
        projectileManager = GetComponentInParent<ProjectileManager>();

        // Destroy the bullet projectile after a certain amount of time
        StartCoroutine(DestroyAfterTime(lifetime));

        // Shoot the bullet projectile in the direction of the player's cursor
        if (parentObj.CompareTag(Tags.Player))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseDir = mousePos - transform.position;
            rb.velocity = new Vector2(mouseDir.x, mouseDir.y).normalized * speed;
        }
        // Shoot the bullet projectile in the player's direction
        else if (parentObj.CompareTag(Tags.Enemy))
        {
            Transform target = GameObject.FindGameObjectWithTag(Tags.Player).transform;
            Vector2 direction = target.position - transform.position;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        }
    }

    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        DestroyProjectile();
    }

    public void OnHitTarget(GameObject hitObject)
    {
        Debug.Log("Bullet hit " + hitObject.name);
        hitObject.GetComponent<IDamageable>().TakeDamage(damageAmount);
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
}
