using UnityEngine;

[System.Serializable]
public struct Range
{
    public float min;
    public float max;

    public float RandomValue => Random.Range(min, max);
}

[RequireComponent(typeof(BaseEnemy))]
[RequireComponent(typeof(BasicMovement))]
public class EnemyController : MonoBehaviour
{
    private BaseEnemy enemy;
    private BasicMovement basicMovement;

    [SerializeField] private Transform target;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Range detectionRange = new Range { min = 3f, max = 4f };
    [SerializeField] private Range reactionTime = new Range { min = 0.8f, max = 1.2f };
    [SerializeField] private Range attackCooldown = new Range { min = 1.25f, max = 2.25f };
    [SerializeField] private Range movementPauseAfterAttack = new Range { min = 0.15f, max = 0.35f };

    private float lastAttackTime = -Mathf.Infinity;
    private float lastMoveAfterAttackTime = -Mathf.Infinity;
    private float currentReactionTime;
    private float currentAttackCooldown;
    private float currentDetectionRange;

    private void Awake()
    {
        enemy = GetComponent<BaseEnemy>();
        basicMovement = GetComponent<BasicMovement>();
        if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;

        // Initialize randomized values
        ResetRandomTimes();
    }

    private void ResetRandomTimes()
    {
        currentReactionTime = reactionTime.RandomValue;
        currentAttackCooldown = attackCooldown.RandomValue;
        currentDetectionRange = detectionRange.RandomValue;
    }

    private void Update()
    {
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);
        float timeSinceAttack = Time.time - lastAttackTime;
        float timeToMove = Time.time - lastMoveAfterAttackTime;

        if (distance <= currentDetectionRange)
        {
            if (distance <= attackRange && timeSinceAttack >= currentAttackCooldown)
            {
                Attack();
                lastMoveAfterAttackTime = Time.time + movementPauseAfterAttack.RandomValue;
                // Reset random times for next cycle
                ResetRandomTimes();
            }
            else if (timeToMove >= currentReactionTime)
            {
                MoveTowardsTarget();
            }
        }
        else
        {
            basicMovement.UpdateDirection(Vector2.zero);
        }
    }

    private void Attack()
    {
        enemy.Attack();
        lastAttackTime = Time.time;
        basicMovement.UpdateDirection(Vector2.zero);
    }

    private void MoveTowardsTarget()
    {
        Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
        basicMovement.UpdateDirection(direction);
    }
}
