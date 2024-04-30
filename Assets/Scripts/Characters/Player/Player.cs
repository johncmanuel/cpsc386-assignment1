using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Invulnerability))]
[RequireComponent(typeof(WeaponManager))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private float interactionRadius = 1f;
    [SerializeField] private TMP_Text weaponEquippedText;
    [SerializeField] private HealthBar healthBar;

    private float maxHealth;

    private Rigidbody2D rb;
    private Invulnerability invulnerability;
    private WeaponManager weaponManager;
    public float Health
    {
        get => health;
        set => health = Mathf.Max(value, 0);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>() ?? GetComponentInChildren<Rigidbody2D>();
        invulnerability = GetComponent<Invulnerability>() ?? GetComponentInChildren<Invulnerability>();
        weaponManager = GetComponent<WeaponManager>() ?? GetComponentInChildren<WeaponManager>();

        if (rb == null)
            Debug.LogError("Couldn't find required Rigidbody2D component");

        if (invulnerability == null)
            Debug.LogError("Couldn't find required Invulnerability component");

        if (weaponManager == null)
            Debug.LogError("Couldn't find required weaponManager component");

        if (healthBar == null)
            Debug.LogError("Couldn't find required HealthBar component");

        if (weaponEquippedText == null)
            Debug.LogError("Couldn't find required weaponEquippedText component");

        // Equip the weapon that the player had equipped before switching scenes
        // if (PlayerData.PlayerGun != null && PlayerData.PlayerGunObj != null)
        // {
        //     Instantiate(PlayerData.PlayerGunObj);
        //     weaponManager.EquipWeapon(PlayerData.PlayerGun);
        // }

        ChangeWeaponEquippedText();
        UpdateHealth();
    }

    private void Update()
    {
        ChangeWeaponEquippedText();
        SavePlayerDataToMemory();
    }

    // Save and track player data in memory throughout the game
    private void SavePlayerDataToMemory()
    {
        // PlayerData.PlayerPosition = transform.position;
        // PlayerData.PlayerHealth = Health;
        // if (weaponManager.CurrentWeapon != null && PlayerData.PlayerGun != null)
        // {
        //     PlayerData.PlayerGun = weaponManager.CurrentWeapon;
        //     PlayerData.PlayerGunObj = ((MonoBehaviour)weaponManager.CurrentWeapon).gameObject;
        // }
    }

    private void UpdateHealth()
    {
        maxHealth = Health;

        if (PlayerData.PlayerHealth > 0)
        {
            Health = PlayerData.PlayerHealth;
            healthBar.UpdateHealthBar(Health / maxHealth);
        }
    }

    private void ChangeWeaponEquippedText()
    {
        // Update the weapon equipped text if anything changed
        if (weaponManager.CurrentWeapon == null)
            weaponEquippedText.text = "Weapon Equipped: None";
        else
            weaponEquippedText.text = "Weapon Equipped: " + weaponManager.CurrentWeapon.ToString();
    }

    public void TakeDamage(float amount)
    {
        if (invulnerability.IsInvulnerable) return;

        Health -= amount;

        healthBar.UpdateHealthBar(Health / maxHealth);

        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.Instance.UpdateGameState(GameStateType.PlayerDied);
    }

    public void Attack()
    {
        weaponManager.AttackWithCurrentWeapon();
    }

    public void TryInteract()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRadius);

        IInteractable nearestInteractable = null;
        float nearestDistance = float.MaxValue;

        foreach (var hit in hits)
        {
            IInteractable interactable = hit.GetComponent<IInteractable>() ?? hit.GetComponentInChildren<IInteractable>();
            if (interactable != null)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestInteractable = interactable;
                }
            }
        }

        if (nearestInteractable != null)
        {
            nearestInteractable.InteractWith(this.gameObject);
        }
    }

    public void UnequipCurrentWeapon()
    {
        weaponManager.UnequipCurrentWeapon();
    }

    private void OnDrawGizmosSelected()
    {
        // visualize the interaction radius in editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
