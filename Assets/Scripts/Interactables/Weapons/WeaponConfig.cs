using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponConfiguration", menuName = "Weapons/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    public ProjectileConfig projectileConfig;
    public float attackCooldown;
}