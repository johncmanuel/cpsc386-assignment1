using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(ProjectilePool))]
public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance { get; private set; }

    private ProjectilePool projectilePool;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        projectilePool = GetComponent<ProjectilePool>() ?? GetComponentInChildren<ProjectilePool>();
        if (projectilePool == null)
            Debug.LogError("Could not find required component ProjectilePool");
    }

    public GameObject SpawnProjectile(string type)
    {
        var proj = projectilePool.GetPooledProjectile(type);
        if (proj == null)
        {
            Debug.LogError("Failed to spawn projectile.");
            return null;
        }
        return proj;
    }

    public void DeleteProjectile(GameObject proj)
    {
        if (proj == null)
        {
            Debug.LogError("Projectile is null.");
            return;
        }

        projectilePool.ReleaseProjectile(proj);
    }
}