using UnityEngine;

public interface IRotationInput
{
    float GetRotationAngle(Vector3 currentPosition);
}

public class MouseRotationInput : IRotationInput
{
    private Camera mainCamera;

    public MouseRotationInput(Camera camera)
    {
        this.mainCamera = camera;
    }

    public float GetRotationAngle(Vector3 currentPosition)
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.z));
        Vector3 rotation = mousePos - currentPosition;
        return Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
    }
}


public class AITargetRotationInput : IRotationInput
{
    private Transform targetTransform;

    public AITargetRotationInput(Transform target)
    {
        this.targetTransform = target;
    }

    public float GetRotationAngle(Vector3 currentPosition)
    {
        Vector3 direction = targetTransform.position - currentPosition;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
}

public class WeaponRotator : MonoBehaviour
{
    private IRotationInput rotationInput;

    public void SetRotationInput(IRotationInput rotationInput)
    {
        this.rotationInput = rotationInput;
    }

    void Update()
    {
        if (rotationInput != null)
        {
            float angle = rotationInput.GetRotationAngle(transform.position);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void OnDrawGizmos()
    {
        if (rotationInput != null)
        {
            Gizmos.color = Color.red;

            float angleInRadians = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0);

            Gizmos.DrawLine(transform.position, transform.position + direction * 2);
        }
    }
}