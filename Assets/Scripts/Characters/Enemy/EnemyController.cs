using UnityEngine;

[RequireComponent(typeof(BaseEnemy))]
[RequireComponent(typeof(BasicMovement))]
public class EnemyController : MonoBehaviour
{
    private BaseEnemy enemy;
    private BasicMovement basicMovement;

    [SerializeField] private Transform target;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float detectionRange = 5f;
    private void Awake()
    {
        enemy = GetComponent<BaseEnemy>();
        basicMovement = GetComponent<BasicMovement>();

        if (enemy == null)
            Debug.LogError("Could not find required enemy component");

        if (basicMovement == null)
            Debug.LogError("Could not find required basicMovement component");
    }

    private void Update()
    {
        if (target == null || basicMovement == null) return;

        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget <= detectionRange)
        {
            if (distanceToTarget > attackRange)
            {
                // Move towards the player
                Vector2 direction = (target.position - transform.position).normalized;
                basicMovement.UpdateDirection(direction);
            }
            else
            {
                // Stop moving before attacking?
                basicMovement.UpdateDirection(Vector2.zero);

                // Attack   
                enemy.Attack();
            }
        }
        else
        {
            // Idle
            basicMovement.UpdateDirection(Vector2.zero);
        }
    }
}