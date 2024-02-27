using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour, ICharacter
{
    public float Health { get; set; }

    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void Move(Vector2 direction)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float amount)
    {
        throw new System.NotImplementedException();
    }
}