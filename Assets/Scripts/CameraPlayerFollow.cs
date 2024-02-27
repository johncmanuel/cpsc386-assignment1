using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform targetPlayerTransform;
    public float followSmoothness = 2f;
    public Vector3 positionOffset;

    void Start()
    {
        if (targetPlayerTransform == null)
        {
            targetPlayerTransform = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        }

        if (targetPlayerTransform == null)
        {
            Debug.LogError("Player not found in the scene!");
        }
    }

    void LateUpdate()
    {
        if (targetPlayerTransform != null)
        {
            Vector3 desiredPosition = targetPlayerTransform.position + positionOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSmoothness * Time.deltaTime);
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, positionOffset.z);
        }
    }
}