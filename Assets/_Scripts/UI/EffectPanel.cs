using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectPanel : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image image;

    private void Start()
    {
        health.OnDamageUI += Health_OnDamageUI;
    }
    private void OnDestroy()
    {
        health.OnDamageUI -= Health_OnDamageUI;
    }

    private void Health_OnDamageUI(float arg1, float arg2)
    {
        StartCoroutine(image.Flash(Color.red, 0.1f, 0.1f, 0.1f, 1));
    }
}
