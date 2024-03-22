using UnityEngine;

public interface IInteractable
{
    bool CanInteract(GameObject objectInteractingWithMe);
    void InteractWith(GameObject objectInteractingWithMe);
}