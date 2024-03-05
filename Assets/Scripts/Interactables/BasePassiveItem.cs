using UnityEngine;

public abstract class BasePassiveItem : MonoBehaviour, IItem
{
    protected abstract void OnPickup();

    public void UseItem()
    {
        OnPickup();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            UseItem();
        }
    }
}