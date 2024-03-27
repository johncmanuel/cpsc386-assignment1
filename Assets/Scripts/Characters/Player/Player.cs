using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Invulnerability))]
[RequireComponent(typeof(WeaponManager))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private float interactionRadius = 1f;
    private float maxHealth;

    private Rigidbody2D rb;
    private Invulnerability invulnerability;
    private WeaponManager weaponManager;

    [SerializeField] private TMP_Text weaponEquippedText;

    [SerializeField] private HealthBar healthBar;

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

        maxHealth = Health;

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

        ChangeWeaponEquippedText();
    }

    private void Update()
    {
        ChangeWeaponEquippedText();
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

        healthBar.UpdateHealthBar(Health / maxHealth);

        Health -= amount;
        if (Health <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
        Debug.Log("Player died!");
        GameManager.Instance.SwitchToScene("GameOverMenu");
        // GameManager.Instance.UpdateGameState(GameStateType.PlayerDied);
    }

    public void Attack()
    {
        weaponManager.AttackWithCurrentWeapon();
    }

    public void InteractWithNearestInteractable()
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

    private void OnDrawGizmosSelected()
    {
        // visualize the interaction radius in editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
