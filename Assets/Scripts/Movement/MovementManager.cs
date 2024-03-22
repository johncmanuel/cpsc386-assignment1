using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private List<IMovementContribution> movementContributors = new List<IMovementContribution>();
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>() ?? GetComponentInChildren<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 finalVelocity = Vector2.zero;
        foreach (var contributor in movementContributors)
        {
            finalVelocity += contributor.GetContribution();
        }
        rb.velocity = finalVelocity;
    }

    public void RegisterContributor(IMovementContribution contributor)
    {
        if (!movementContributors.Contains(contributor))
        {
            movementContributors.Add(contributor);
        }
    }

    public void UnregisterContributor(IMovementContribution contributor)
    {
        if (movementContributors.Contains(contributor))
        {
            movementContributors.Remove(contributor);
        }
    }
}
