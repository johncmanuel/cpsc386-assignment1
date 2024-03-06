using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(WeaponManager))]
public abstract class BaseEnemy : MonoBehaviour, IDamageable, ICombatant
{
    [SerializeField] private float health;
    private IWeapon weaponToEquip;

    private WeaponManager weaponManager;

    public float Health
    {
        get => health;
        set => health = Mathf.Max(value, 0);
    }

    public void Start()
    {
        weaponManager = GetComponent<WeaponManager>() ?? GetComponentInChildren<WeaponManager>();
        if (weaponManager == null)
            Debug.LogError("Couldn't find required weaponManager component");

        weaponToEquip = GetComponent<IWeapon>() ?? GetComponentInChildren<IWeapon>();
        if (weaponToEquip == null)
            Debug.LogError("Couldn't find required IWeapon inherited component in children");

        weaponManager.EquipWeapon(weaponToEquip);
    }

    public abstract void Attack();

    public virtual void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Die();
        }
        Debug.Log($"Enemy took {amount} damage. Remaining health: {Health}");
    }

    public virtual void Die()
    {
        Debug.Log("Enemy Died!");
        // Implement death logic here, like playing an animation or disabling the enemy
    }
}
