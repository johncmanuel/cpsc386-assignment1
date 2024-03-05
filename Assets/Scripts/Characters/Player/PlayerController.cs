using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(BasicMovement))]
[RequireComponent(typeof(DashMovement))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float interactionRadius = 1f;

    private Player player;
    private Vector2 movementInput;
    private BasicMovement basicMovement;
    private DashMovement dashMovement;

    private void Start()
    {
        player = GetComponent<Player>();
        basicMovement = GetComponent<BasicMovement>();
        dashMovement = GetComponent<DashMovement>();

        ValidateComponents();
    }

    private void ValidateComponents()
    {
        if (player == null)
            Debug.LogError("Couldn't find required Player component");
        if (basicMovement == null)
            Debug.LogError("Couldn't find required BasicMovement component");
        if (dashMovement == null)
            Debug.LogError("Couldn't find required DashMovement component");
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
            player.Attack();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            player.InteractWithNearestInteractable();
        }
    }
}
