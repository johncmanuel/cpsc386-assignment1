public interface IWeapon : IInteractable
{
    bool CanBeEquipped { get; set; }
    void Attack();
}
