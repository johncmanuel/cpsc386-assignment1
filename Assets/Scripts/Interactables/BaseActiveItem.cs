using UnityEngine;

public abstract class BaseActiveItem : MonoBehaviour, IItem, IInteractable
{
    protected abstract void OnUse();

    public void UseItem()
    {
        OnUse();
        // handle item consumption or cooldown?
    }

    public void InteractWith()
    {
        UseItem();
    }
}
