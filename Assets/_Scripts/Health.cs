using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<float, float> OnHeal;
    public event Action<float, float> OnDamageUI;
    public event Action<float, Vector3> OnDamage;
    public event Action<Vector3> OnDead;

    public float maxHealth;
    public float health;

    [SerializeField] private float invinciblePeriod = 0f;

    private float lastDamageTime;
    public bool IsInvincible => Time.realtimeSinceStartup - lastDamageTime < invinciblePeriod;

    public bool IsDead { get; private set; } = false;

    public void Heal(float amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;
        OnHeal?.Invoke(maxHealth, health);
    }
    public void Revive()
    {
        health = maxHealth;
        IsDead = false;
        OnHeal?.Invoke(maxHealth, health);
    }
    public void TakeDamage(float amount, Vector3 damageDirection)
    {
        if (health > 0)
        {
            if (IsInvincible == false)
            {
                lastDamageTime = Time.realtimeSinceStartup;
                health -= amount;
                health = Mathf.Clamp(health, 0, maxHealth);
                OnDamage?.Invoke(health, damageDirection);
                OnDamageUI?.Invoke(maxHealth, health);
            }
        }
        if (health <= 0 && IsDead == false)
        {
            Kill(damageDirection);
        }
    }

    public void Kill(Vector3 damageDirection)
    {
        IsDead = true;
        OnDead?.Invoke(damageDirection);
    }
}
