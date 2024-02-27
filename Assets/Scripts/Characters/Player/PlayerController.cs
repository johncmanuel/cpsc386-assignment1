using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Dash))]
public class PlayerController : MonoBehaviour
{
    private Player player;
    private Dash dashComponent;
    private Vector2 movementInput;

    private void Awake()
    {
        player = GetComponent<Player>();
        if (player == null)
            Debug.LogError("Couldnt find required player component");

        dashComponent = GetComponent<Dash>();
        if (dashComponent == null)
            Debug.LogError("Couldnt find required dash component");
    }

    private void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        player.Move(movementInput);
    }

    private void ProcessInputs()
    {
        movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(dashComponent.PerformDash(movementInput.normalized));
        }
    }
}