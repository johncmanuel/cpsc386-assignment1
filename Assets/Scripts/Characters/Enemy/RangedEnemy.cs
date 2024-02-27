using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    public override void Attack()
    {
        // Implement range-specific attack logic here
        Debug.Log("Ranged Enemy Attacks!");
    }
}