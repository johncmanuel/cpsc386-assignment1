using UnityEngine;

public abstract class BaseGun : MonoBehaviour, IWeapon
{
    private ProjectileManager projectileManager;
    private string projectileType = "Bullet";
    public bool CanBeEquipped { get; set; } = true;

    [SerializeField] private Transform bulletSpawn;

    private void Start()
    {
        projectileManager = ProjectileManager.Instance;
        if (projectileManager == null) Debug.LogError("Couldn't find required ProjectileManager component");
    }

    public void Attack()
    {
        if (CanBeEquipped)
        {
            projectileManager.SpawnProjectile(projectileType);
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
