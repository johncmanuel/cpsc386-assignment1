using UnityEngine;

[RequireComponent(typeof(ProjectilePool))]
public class ProjectileManager : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    public ProjectilePool projectilePool { get; protected set; }

    [SerializeField]
    private int maxProjectiles = 100;
    private int currentProjectiles = 0;
    // private int projectilesPerSecond = 1;
    public bool isUsingPool = false;

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize the pool
        projectilePool = GetComponent<ProjectilePool>();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public GameObject SpawnProjectile()
    {
        GameObject proj;

        if (isUsingPool)
        {
            proj = projectilePool.pool.Get();
        }
        else
        {
            proj = Instantiate(projectilePrefab, transform);
        }

        if (proj == null)
        {
            Debug.LogError("Projectile is null");
        }

        Debug.Log("Spawning projectile");

        currentProjectiles++;

        // Spawn projectile at an entity's position.
        proj.transform.position = gameObject.transform.position;
        return proj;
    }
}
