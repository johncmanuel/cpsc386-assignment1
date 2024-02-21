using UnityEngine;

public interface ICharacter
{
    float Health { get; set; }
    void Move(Vector2 direction);
    void TakeDamage(float amount);
    void Attack();
}

public interface IAimable
{
    void AimTowards(Vector2 aimDirection);
}
