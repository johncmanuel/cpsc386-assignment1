using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> projectilePrefabs = new List<GameObject>();

    private Dictionary<string, ObjectPool<GameObject>> pools = new Dictionary<string, ObjectPool<GameObject>>();

    private void Start()
    {
        InitializePools();
    }

    private void InitializePools()
    {
        foreach (var prefab in projectilePrefabs)
        {
            IProjectile projectileScript = prefab.GetComponent<IProjectile>();
            if (projectileScript == null)
            {
                Debug.LogWarning($"Prefab {prefab.name} does not have an IProjectile component. Check ProjectilePool in inspector.");
                continue;
            }

            string type = projectileScript.Type;
            var pool = new ObjectPool<GameObject>(
                createFunc: () => {
                    var instance = Instantiate(prefab);
                    instance.name = prefab.name;
                    return instance;
                },
                actionOnGet: (obj) => obj.SetActive(true),
                actionOnRelease: (obj) => obj.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: false,
                defaultCapacity: 10,
                maxSize: 100);
            pools[type] = pool;
        }
    }

    public GameObject GetPooledProjectile(string type)
    {
        if (pools.TryGetValue(type, out var pool))
        {
            return pool.Get();
        }

        Debug.LogError($"No pool found for projectile type: {type}");
        return null;
    }

    public void ReleaseProjectile(GameObject projectile)
    {
        IProjectile projectileComponent = projectile.GetComponent<IProjectile>();
        if (projectileComponent != null && pools.TryGetValue(projectileComponent.Type, out var pool))
        {
            pool.Release(projectile);
        }
        else
        {
            Debug.LogError($"Attempted to release unmanaged projectile or projectile without IProjectile component: {projectile.name}");
        }
    }
}
