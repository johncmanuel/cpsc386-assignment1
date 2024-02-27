using UnityEngine;

[RequireComponent(typeof(BaseEnemy))]
public class EnemyController : MonoBehaviour
{
    private BaseEnemy enemy;
    [SerializeField] private Transform target;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float detectionRange = 5f;

    private void Awake()
    {
        enemy = GetComponent<BaseEnemy>();
        // null check this TODO
    }

    private void Update()
    {
        if (target == null) return;

        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget <= detectionRange)
        {
            if (distanceToTarget > attackRange)
            {
                // Move towards the player
                Vector2 direction = (target.position - transform.position).normalized;
                enemy.Move(direction);
            }
            else
            {
                // Attack the player
                enemy.Attack();
                // Optionally, stop moving when attacking
                enemy.Move(Vector2.zero);
            }
        }
        else
        {
            // Idle or patrol logic
            enemy.Move(Vector2.zero);
        }
    }
}
