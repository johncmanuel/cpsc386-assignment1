using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    private new void Start()
    {
        base.Start();
    }

    public override void Attack()
    {
        weaponManager.AttackWithCurrentWeapon();
    }
}