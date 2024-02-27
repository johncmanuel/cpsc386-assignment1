using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    public ObjectPool<GameObject> pool { get; set; }

    private void Awake()
    {
        pool = new ObjectPool<GameObject>(CreateProjectile, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject);
    }
    private GameObject CreateProjectile()
    {
        GameObject proj = Instantiate(projectilePrefab);
        proj.SetActive(false);
        proj.transform.SetParent(transform);
        return proj;
    }

    private void OnReleaseToPool(GameObject pooledProjectile)
    {
        pooledProjectile.gameObject.SetActive(false);
    }

    private void OnGetFromPool(GameObject pooledProjectile)
    {
        pooledProjectile.gameObject.SetActive(true);
    }

    private void OnDestroyPooledObject(GameObject pooledProjectile)
    {
        Destroy(pooledProjectile.gameObject);
    }

}
