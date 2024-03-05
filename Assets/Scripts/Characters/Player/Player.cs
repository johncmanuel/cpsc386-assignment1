using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Invulnerability))]
[RequireComponent(typeof(WeaponManager))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private float interactionRadius = 1f;

    private Rigidbody2D rb;
    private ProjectileManager projectileManager;
    private Invulnerability invulnerability;
    private WeaponManager weaponManager;


    public float Health
    {
        get => health;
        set => health = Mathf.Max(value, 0);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        invulnerability = GetComponent<Invulnerability>();
        projectileManager = ProjectileManager.Instance;
        weaponManager = GetComponent<WeaponManager>();

        if (rb == null)
            Debug.LogError("Couldn't find required Rigidbody2D component");

        if (invulnerability == null)
            Debug.LogError("Couldn't find required Invulnerability component");

        if (projectileManager == null)
            Debug.LogError("Couldn't find required ProjectileManager component");

        if (weaponManager == null)
            Debug.LogError("Couldn't find required weaponManager component");
    }

    public void TakeDamage(float amount)
    {
        if (invulnerability.IsInvulnerable) return;

        Health -= amount;
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player died!!");
        GameManager.Instance.UpdateGameState(GameStateType.PlayerDied);
    }

    public void EquipWeapon(IWeapon weapon)
    {
        weaponManager.EquipWeapon(weapon);
    }

    public void Attack()
    {
        weaponManager.AttackWithCurrentWeapon();
        projectileManager.SpawnProjectile("Bullet");
    }

    public void InteractWithNearestInteractable()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRadius);

        IInteractable nearestInteractable = null;
        float nearestDistance = float.MaxValue;

        foreach (var hit in hits)
        {
            IInteractable interactable = hit.GetComponent<IInteractable>();
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
            nearestInteractable.InteractWith();
        }
    }

    private void OnDrawGizmosSelected()
    {
        // visualize the interaction radius in editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
