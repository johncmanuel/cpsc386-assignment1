using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private string[] collisionCheckTags = {"Projectile"};
    void OnCollisionEnter(Collision collision)
    {
        // check for collision with desired collision tags
        foreach (var tag in collisionCheckTags)
        {
            if (collision.gameObject.tag == tag)
            {
                var projectile = collision.gameObject.GetComponent<IProjectile>();
                
                if (projectile != null)
                {
                    // projectile.OnHitTarget();
                    Debug.Log("Hitbox received hit from: " + collision.gameObject.name);
                }
            }
        }
    }
}