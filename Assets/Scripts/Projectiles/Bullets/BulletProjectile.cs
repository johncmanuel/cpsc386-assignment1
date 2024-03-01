using UnityEngine;

public class BulletProjectile : MonoBehaviour, IProjectile
{
    [SerializeField]
    private float damageAmount = 10f;
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float lifetime = 7f;

    private Rigidbody2D rb;
    private Vector3 mousePos;

    public void OnHitTarget(GameObject hitObject)
    {
        Debug.Log("Bullet hit " + hitObject.name);
    }

    public void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Debug.Log("Bullet destroyed");
    }

    public void DetectCollision(GameObject target)
    {
        Debug.Log("Bullet collided with " + target.name);

        if (target.CompareTag("Player"))
        {
            // deal damage to the player
            target.GetComponent<Player>().TakeDamage(damageAmount);
        }
        else if (target.CompareTag("Enemy"))
        {
            // deal damage to the enemy
            target.GetComponent<BaseEnemy>().TakeDamage(damageAmount);
        }

        DestroyProjectile();
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        var parentObj = transform.parent.gameObject;

        // Shoot the bullet projectile in the direction of the player's cursor
        if (parentObj.CompareTag("Player"))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
        // if (lifetime <= 0)
        // {
        //     Debug.Log("Bullet lifetime reached 0. Destroying bullet.");
        //     DestroyProjectile();
        // }
        // else
        // {
        //     lifetime -= Time.deltaTime;
        // }
    }
}
