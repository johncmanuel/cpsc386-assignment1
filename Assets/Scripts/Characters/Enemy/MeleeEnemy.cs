using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    public override void Attack()
    {
        // Implement melee-specific attack logic here
        Debug.Log("Melee Enemy Attacks!");
    }
}