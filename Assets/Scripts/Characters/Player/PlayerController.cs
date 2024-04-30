using Unity.Burst.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(BasicMovement))]
[RequireComponent(typeof(DashMovement))]
public class PlayerController : MonoBehaviour
{
    private Player player;
    private Vector2 movementInput;
    private BasicMovement basicMovement;
    private DashMovement dashMovement;
    private WeaponRotator weaponRotator;

    [SerializeField] private Animator animator;

    private void Start()
    {
        // Get Components
        player = GetComponent<Player>() ?? GetComponentInChildren<Player>();
        basicMovement = GetComponent<BasicMovement>() ?? GetComponentInChildren<BasicMovement>();
        dashMovement = GetComponent<DashMovement>() ?? GetComponentInChildren<DashMovement>();
        weaponRotator = GetComponent<WeaponRotator>() ?? GetComponentInChildren<WeaponRotator>();

        // Check them
        if (player == null)
            Debug.LogError("Couldn't find required Player component");
        if (basicMovement == null)
            Debug.LogError("Couldn't find required BasicMovement component");
        if (dashMovement == null)
            Debug.LogError("Couldn't find required DashMovement component");
        if (weaponRotator == null)
            Debug.LogError("Couldn't find required WeaponRotator component");

        // Set them up
        IRotationInput mouseRotationInput = new MouseRotationInput(Camera.main);
        weaponRotator.SetRotationInput(mouseRotationInput);
    }

    private void Update()
    {
        ProcessInputs();
        Animate();
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
            player.Attack();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            player.TryInteract();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            // drop weapon
            player.UnequipCurrentWeapon();
        }
    }

    private void Animate()
    {
        animator.SetFloat(PlayerAnim.AnimMoveX, movementInput.x);
        animator.SetFloat(PlayerAnim.AnimMoveY, movementInput.y);
        animator.SetFloat(PlayerAnim.AnimMoveMagnitude, movementInput.magnitude);
    }
}
