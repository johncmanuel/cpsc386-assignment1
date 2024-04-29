using System.Collections;
using UnityEngine;

public abstract class BaseGun : MonoBehaviour, IWeapon
{
    public bool CanBeEquipped { get; set; } = true;

    private ProjectileManager projectileManager;
    private string projectileType = "Bullet";
    private bool canAttack = true;

    [SerializeField] private float attackCooldown = 0.1f;
    [SerializeField] private float bulletSpeed = 1f;
    [SerializeField] private Transform bulletSpawn;

    private bool attackQueued = false;

    private void Start()
    {
        projectileManager = ProjectileManager.Instance;
        if (projectileManager == null) Debug.LogError("Couldn't find required ProjectileManager component");

        if (!bulletSpawn) Debug.LogError("The Bullet Spawn transform for the weapon needs to be set in the inspector.");
    }

    public void Attack()
    {
        if (CanBeEquipped) return;

        if (!canAttack)
        {
            attackQueued = true;
            return;
        }

        PerformAttack();
        StartCoroutine(StartSound());
        StartCoroutine(StartAttackCooldown());
    }

    private void PerformAttack()
    {
        GameObject bullet = projectileManager.SpawnProjectile(projectileType);

        Vector2 spawnPosition = bulletSpawn.position;
        Quaternion spawnRotation = bulletSpawn.rotation;

        bullet.transform.position = spawnPosition;
        bullet.transform.rotation = spawnRotation;

        Vector2 attackVelocity = bulletSpawn.right * bulletSpeed;

        bullet.GetComponent<Rigidbody2D>().velocity = attackVelocity;
    }

    private IEnumerator StartAttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;

        if (attackQueued)
        {
            attackQueued = false;
            PerformAttack();
        }
    }

    public bool CanInteract(GameObject objectInteractingWithMe)
    {
        return CanBeEquipped;
    }

    public void InteractWith(GameObject objectInteractingWithMe)
    {
        EquipWeapon(objectInteractingWithMe);
    }

    private void EquipWeapon(GameObject objectInteractingWithMe)
    {
        WeaponManager weaponManager = objectInteractingWithMe.GetComponent<WeaponManager>();
        if (weaponManager != null && CanBeEquipped)
        {
            weaponManager.EquipWeapon(this);
            CanBeEquipped = false;
        }
    }

    private IEnumerator StartSound()
    {
        PlaySound();
        yield return null;
    }

    protected abstract void PlaySound();
}
