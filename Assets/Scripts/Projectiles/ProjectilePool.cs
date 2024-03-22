using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public interface IPoolsObjectTypes
{
    GameObject GetPooledObjectByType(string type);
    void ReleasePooledObject(GameObject pooledObject);
}

public class ProjectilePool : MonoBehaviour, IPoolsObjectTypes
{
    [SerializeField]
    private List<GameObject> projectilePrefabs;

    private readonly Dictionary<string, ObjectPool<GameObject>> projectilePools = new();
    
    private const int InitialPoolCapacity = 10;
    private const int MaxPoolSize = 100;

    private void Start()
    {
        InitializeProjectilePools();
    }

    private void InitializeProjectilePools()
    {
        foreach (var prefab in projectilePrefabs)
        {
            var projectileScript = prefab.GetComponent<IProjectile>() ?? prefab.GetComponentInChildren<IProjectile>();
            if (projectileScript == null)
            {
                Debug.LogWarning($"Prefab {prefab.name} does not have an IProjectile component. Check ProjectilePoolManager in inspector.");
                continue;
            }

            projectilePools[projectileScript.Type] = CreatePoolForPrefab(prefab);
        }
    }

    private ObjectPool<GameObject> CreatePoolForPrefab(GameObject prefab)
    {
        return new ObjectPool<GameObject>(
            createFunc: () => InstantiatePrefab(prefab),
            actionOnGet: ActivateGameObject,
            actionOnRelease: DeactivateGameObject,
            actionOnDestroy: GameObject.Destroy,
            collectionCheck: false,
            defaultCapacity: InitialPoolCapacity,
            maxSize: MaxPoolSize);
    }

    private GameObject InstantiatePrefab(GameObject prefab)
    {
        var instance = Instantiate(prefab);
        instance.name = prefab.name;
        return instance;
    }

    private static void ActivateGameObject(GameObject obj) => obj.SetActive(true);
    private static void DeactivateGameObject(GameObject obj) => obj.SetActive(false);

    public GameObject GetPooledObjectByType(string type)
    {
        if (projectilePools.TryGetValue(type, out var pool))
        {
            return pool.Get();
        }

        Debug.LogError($"No pool found for projectile type: {type}");
        return null;
    }

    public void ReleasePooledObject(GameObject projectile)
    {
        var projectileComponent = projectile.GetComponent<IProjectile>() ?? projectile.GetComponentInChildren<IProjectile>(true);
        if (projectileComponent != null && projectilePools.TryGetValue(projectileComponent.Type, out var pool))
        {
            pool.Release(projectile);
        }
        else
        {
            Debug.LogError($"Attempted to release unmanaged projectile or projectile without IProjectile component: {projectile.name}");
        }
    }
}