using UnityEngine;

public class BulletProjectile : MonoBehaviour, IProjectile
{
    [SerializeField]
    float damageAmount = 10;
    [SerializeField]
    float speed = 2;

    public void OnHitTarget(GameObject hitObject)
    {
        Debug.Log("Bullet hit " + hitObject.name);
    }

    public void DestroyProjectile()
    {
        // Deactivate or destroy the bullet projectile
        Debug.Log("Deactivated Bullet ");
    }

    public void DetectCollision(GameObject target)
    {
        if (target.tag == "Player")
        {
            // hit the player
            // ...
            // then destroy or deactivate the projectile
            DestroyProjectile();
        }
    }

    private void Update()
    {
        // move the bullet projectile in weapon's direction until collision is detected
        transform.Translate(new Vector3(speed * Time.deltaTime, 0f, 0f));
        Debug.Log("BulletProjectile moving...");
    }
}
