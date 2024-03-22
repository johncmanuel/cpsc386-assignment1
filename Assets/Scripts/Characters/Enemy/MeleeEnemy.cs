using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    private new void Start()
    {
        base.Start();
    }

    public override void Attack()
    {
        // Implement melee-specific attack logic here
        Debug.Log("Melee Enemy Attacks!");
    }
}