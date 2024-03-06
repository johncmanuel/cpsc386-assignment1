using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWeapon : MonoBehaviour
{
    // Source utilized:
    // https://www.youtube.com/watch?v=-bkmPm_Besk

    private Camera mainCamera;
    private Vector3 mousePos;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag(Tags.MainCamera).GetComponent<Camera>();

        if (mainCamera == null)
            Debug.LogError("Couldn't find required Camera component");
    }

    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.z));
        Vector3 rotation = mousePos - transform.position;
        float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
