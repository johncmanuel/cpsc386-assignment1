using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(WeaponManager))]
public abstract class BaseEnemy : MonoBehaviour, IDamageable, ICombatant
{
    [SerializeField] protected float health;

    protected IWeapon weaponToEquip;
    protected WeaponManager weaponManager;
    protected WeaponRotator weaponRotator;
    protected float unequipProbability = 0.25f;

    public float Health
    {
        get => health;
        set => health = Mathf.Max(value, 0);
    }

    public void Start()
    {
        // Get Components
        weaponManager = GetComponent<WeaponManager>() ?? GetComponentInChildren<WeaponManager>();
        weaponToEquip = GetComponent<IWeapon>() ?? GetComponentInChildren<IWeapon>();
        weaponRotator = GetComponent<WeaponRotator>() ?? GetComponentInChildren<WeaponRotator>();

        // Check them
        if (weaponManager == null)
            Debug.LogError("Couldn't find required weaponManager component");
        if (weaponToEquip == null)
            Debug.LogError("Couldn't find required IWeapon inherited component in children");
        if (weaponRotator == null)
        {
            Debug.LogError("Couldn't find required WeaponRotator component");
            return;
        }

        // Set them up
        weaponManager.EquipWeapon(weaponToEquip);

        GameObject playerObject = GameObject.FindGameObjectWithTag(Tags.Player);
        if (playerObject != null)
        {
            Transform playerTransform = playerObject.transform;
            IRotationInput aiTargetRotationInput = new AITargetRotationInput(playerTransform);
            weaponRotator.SetRotationInput(aiTargetRotationInput);
        }
        else
        {
            Debug.LogError("Player object not found in the scene.");
        }
    }

    public abstract void Attack();

    public virtual void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Die();
        }
        else Debug.Log($"Enemy took {amount} damage. Remaining health: {Health}");
    }

    public virtual void Die()
    {
        Debug.Log("Enemy Died!");
        // Implement death logic

        float randomValue = Random.value; // between 0 and 1

        // Check if the random value is within the probability of unequipping the weapon
        if (randomValue < unequipProbability)
        {
            IWeapon currentWeapon = weaponManager.CurrentWeapon;

            weaponManager.UnequipCurrentWeapon();
        }

        Destroy(this.gameObject);
    }
}
