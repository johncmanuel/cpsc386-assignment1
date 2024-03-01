using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ProjectileManager))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(BasicMovement))]
[RequireComponent(typeof(DashMovement))]
public class PlayerController : MonoBehaviour
{
    private Player player;
    private Vector2 movementInput;
    private BasicMovement basicMovement;
    private DashMovement dashMovement;
    private ProjectileManager projectileManager;

    private void Awake()
    {
        player = GetComponent<Player>();
        basicMovement = GetComponent<BasicMovement>();
        dashMovement = GetComponent<DashMovement>();
        projectileManager = GetComponent<ProjectileManager>();

        if (player == null)
            Debug.LogError("Couldnt find required player component");

        if (basicMovement == null)
            Debug.LogError("Couldn't find required BasicMovement component");

        if (basicMovement == null)
            Debug.LogError("Couldn't find required DashMovement component");

        if (projectileManager == null)
            Debug.LogError("Couldn't find required ProjectileManager component");
    }

    private void Update()
    {
        ProcessInputs();
    }

    private void ProcessInputs()
    {
        movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        basicMovement.UpdateDirection(movementInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dashMovement.PerformDash(movementInput.normalized);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            projectileManager.SpawnProjectile();
        }
    }
}