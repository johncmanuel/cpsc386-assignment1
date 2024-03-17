using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBarImage;

    // Any entity that implements IDamageable
    [SerializeField] private GameObject entity;

    // Start is called before the first frame update
    void Start()
    {
        healthBarImage = GetComponent<Image>();

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

        UpdateHealthBar(damagable.Health);
    }

    public void UpdateHealthBar(float health)
    {
        healthBarImage.fillAmount = Mathf.Clamp01(health);
    }

}
