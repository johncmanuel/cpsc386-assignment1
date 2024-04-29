using UnityEngine;

public class ToggleableHitbox : Hitbox
{
    private bool isActive = true;
    public void ToggleActive()
    {
        isActive = !isActive;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive)
            return;

        base.OnCollisionEnter2D(collision);
    }
}
