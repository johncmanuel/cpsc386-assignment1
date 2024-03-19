using UnityEngine;

public abstract class BaseGun : MonoBehaviour, IWeapon
{
    private ProjectileManager projectileManager;
    private string projectileType = "Bullet";
    public bool CanBeEquipped { get; set; } = true;

    [SerializeField] private float bulletSpeed = 1f;

    [SerializeField] private Transform bulletSpawn;

    private void Start()
    {
        projectileManager = ProjectileManager.Instance;
        if (projectileManager == null) Debug.LogError("Couldn't find required ProjectileManager component");

        if (!bulletSpawn) Debug.LogError("The Bullet Spawn transform for the weapon needs to be set in the inspector.");
    }

    public void Attack()
    {
        // Only attack when we are equipped
        if (!CanBeEquipped)
        {
            GameObject bullet = projectileManager.SpawnProjectile(projectileType);

            // bulletSpawn is a Transform representing the exact point of bullet spawning
            Vector3 spawnPosition = bulletSpawn.position;
            Quaternion spawnRotation = bulletSpawn.rotation;

            // Set the bullets position and rotation to match the bulletSpawns
            bullet.transform.position = spawnPosition;
            bullet.transform.rotation = spawnRotation;

            // Use the bulletSpawns forward direction as the attack direction, assuming bulletSpawn is oriented correctly
            Vector3 attackVelocity = bulletSpawn.forward * bulletSpeed;

            bullet.GetComponent<Rigidbody2D>().velocity = attackVelocity;
        }
    }


    public bool CanInteract(GameObject objectInteractingWithMe)
    {
        return CanBeEquipped;
    }

    public void InteractWith(GameObject objectInteractingWithMe)
    {
        // Ensure the object interacting has a WeaponManager to equip the weapon
        WeaponManager weaponManager = objectInteractingWithMe.GetComponent<WeaponManager>();
        if (weaponManager != null && CanBeEquipped)
        {
            weaponManager.EquipWeapon(this);
            CanBeEquipped = false;
        }
    }
}