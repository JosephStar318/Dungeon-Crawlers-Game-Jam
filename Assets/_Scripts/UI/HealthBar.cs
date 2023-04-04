using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Slider slider;

    private void Start()
    {
        health.OnDamageUI += Health_OnDamageUI;
        health.OnHeal += Health_OnHeal;
    }
    private void OnDestroy()
    {
        health.OnDamageUI += Health_OnDamageUI;
        health.OnHeal -= Health_OnHeal;
    }
    private void Health_OnDamageUI(float maxHealth, float health)
    {
        slider.value = Mathf.InverseLerp(0, maxHealth, health) * 10;
    }
    private void Health_OnHeal(float maxHealth, float health)
    {
        slider.value = Mathf.InverseLerp(0, maxHealth, health) * 10;
    }
}
