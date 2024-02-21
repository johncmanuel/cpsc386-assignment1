using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private PlayerCharacter playerCharacter;
    private Vector2 movementInput;
    private Vector2 aimDirection;
    [SerializeField]
    private float dashDistance = 5.0f;
    [SerializeField]
    private float dashCooldown = 2.0f;
    [SerializeField]
    private float invulnerabilityDuration = 1.5f;
    private bool canDash = true;

    private void Awake()
    {
        playerCharacter = GetComponent<PlayerCharacter>();
    }

    private void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        playerCharacter.Move(movementInput);
    }

    private void ProcessInputs()
    {
        movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        aimDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aimDirection.Normalize();

        if (Input.GetButtonDown("Fire1"))
        {
            playerCharacter.AimTowards(aimDirection);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        playerCharacter.SetInvulnerability(true, invulnerabilityDuration);

        // Calculate dash target and use Rigidbody for movement
        Vector2 start = transform.position;
        Vector2 end = start + aimDirection * dashDistance;
        float dashTime = 0.1f;
        float elapsedTime = 0;

        while (elapsedTime < dashTime)
        {
            playerCharacter.Move(aimDirection * (dashDistance / dashTime) * Time.fixedDeltaTime);
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(dashCooldown - dashTime); // Adjust cooldown based on dashTime
        canDash = true;
    }
}
