using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    public ProjectilePool projectilePool { get; set; }

    [SerializeField]
    private int maxProjectiles = 10;
    // private int currentProjectiles = 0;
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
        // if player triggers the gun, spawn a projectile here
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnProjectile();
            Debug.Log("Spawned projectile");
        }
    }

    private GameObject SpawnProjectile()
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
        // Spawn projectile at player's position here.
        // ...
        proj.transform.position = new Vector3(0, 0, 0);
        return proj;
    }
}
