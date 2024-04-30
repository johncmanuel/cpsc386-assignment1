using UnityEngine;

public class Shotgun : BaseGun
{
    [SerializeField] private const float spread = 0.15f;
    [SerializeField] private int pelletsCount = 6;

    protected override void PerformAttack()
    {
        for (int i = 0; i < pelletsCount; i++)
        {
            GameObject pellet = projectileManager.SpawnProjectile(weaponConfig.projectileConfig.type);
            pellet.transform.position = projectileSpawn.position;
            pellet.transform.rotation = projectileSpawn.rotation;

            // Randomize spread
            Vector2 attackDirection = (Vector2)projectileSpawn.right + Random.insideUnitCircle * spread;
            pellet.GetComponent<Rigidbody2D>().velocity = attackDirection * weaponConfig.projectileConfig.speed;
        }
    }

    protected override void PlaySound()
    {
        AudioManager.Instance.Play(AudioNames.ShotgunSoundName);
    }
}