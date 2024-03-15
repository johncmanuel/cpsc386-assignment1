using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private RectTransform healthBarRect;

    [SerializeField]
    private GameObject entity;

    // Start is called before the first frame update
    void Start()
    {
        healthBarRect = GetComponent<RectTransform>();

        if (entity == null)
        {
            Debug.LogError("Entity is not set in the HealthBar component");
            return;
        }

        if (!entity.TryGetComponent<IDamageable>(out var damagable))
        {
            Debug.LogError("Entity does not implement IDamageable");
            return;
        }

        UpdateHealthBarSize(damagable.Health / 10f);

    }

    public void UpdateHealthBarSize(float size)
    {
        healthBarRect.localScale = new Vector3(size, 1f);
    }

}
