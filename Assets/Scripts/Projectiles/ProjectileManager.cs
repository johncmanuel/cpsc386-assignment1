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

        // Debug.Log("Spawning projectile");

        currentProjectiles++;

        if (gameObject.CompareTag("Player"))
        {
            // Spawn projectile at player weapon's position
            GameObject childObj = gameObject.transform.Find("RotationPoint/Weapon").gameObject;
            proj.transform.position = childObj.transform.position;
        }
        else
        {
            // Spawn projectile at parent object's position
            proj.transform.position = gameObject.transform.position;
        }


        return proj;
    }

    public void DeleteProjectile(GameObject proj)
    {
        if (proj == null)
        {
            Debug.LogError("Projectile is null");
            return;
        }

        if (isUsingPool)
        {
            projectilePool.pool.Release(proj);
        }
        else
        {
            Destroy(proj);
        }

        Debug.Log("Deleting projectile: " + proj.name);

        currentProjectiles--;
    }
}
