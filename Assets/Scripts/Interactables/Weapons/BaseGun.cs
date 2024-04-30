using System.Collections;
using UnityEngine;

public abstract class BaseGun : MonoBehaviour, IWeapon
{
    public bool CanBeEquipped { get; set; } = true;

    protected ProjectileManager projectileManager;
    protected bool canAttack = true;

    [SerializeField] protected WeaponConfig weaponConfig;
    [SerializeField] protected Transform projectileSpawn;

    private bool attackQueued = false;

    private void Start()
    {
        projectileManager = ProjectileManager.Instance;
        if (projectileManager == null) Debug.LogError("Couldn't find required ProjectileManager component");

        if (!projectileSpawn) Debug.LogError("The Bullet Spawn transform for the weapon needs to be set in the inspector.");
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

    protected virtual void PerformAttack()
    {
        GameObject bullet = projectileManager.SpawnProjectile(weaponConfig.projectileConfig.type);

        Vector2 spawnPosition = projectileSpawn.position;
        Quaternion spawnRotation = projectileSpawn.rotation;

        bullet.transform.position = spawnPosition;
        bullet.transform.rotation = spawnRotation;

        Vector2 attackVelocity = projectileSpawn.right * weaponConfig.projectileConfig.speed;

        bullet.GetComponent<Rigidbody2D>().velocity = attackVelocity;
    }

    private IEnumerator StartAttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(weaponConfig.attackCooldown);
        canAttack = true;

        if (attackQueued)
        {
            attackQueued = false;
            Attack();
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
