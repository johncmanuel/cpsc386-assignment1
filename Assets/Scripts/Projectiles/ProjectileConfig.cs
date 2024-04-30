using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileConfiguration", menuName = "Projectile/ProjectileConfig")]
public class ProjectileConfig : ScriptableObject
{
    public ProjectileType type;
    public float speed;
    public float damage;
    public Sprite sprite;
    public float lifetime;
}