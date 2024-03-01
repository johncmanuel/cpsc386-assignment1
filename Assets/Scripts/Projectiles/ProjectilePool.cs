using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    public ObjectPool<GameObject> pool { get; protected set; }

    private void OnEnable()
    {
        pool = new ObjectPool<GameObject>(CreateProjectile, OnGetFromPool, OnReleaseToPool, OnDestroyPooledProjectile);
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

    private void OnDestroyPooledProjectile(GameObject pooledProjectile)
    {
        Destroy(pooledProjectile.gameObject);
    }

}
