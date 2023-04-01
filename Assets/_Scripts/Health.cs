using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<float, float> OnHeal;
    public event Action<float, float> OnDamageUI;
    public event Action<float, Vector2> OnDamage;
    public event Action<Vector2> OnDead;

    public float maxHealth;
    public float health;
    public bool isDead = false;

    [SerializeField] private float invinciblePeriod = 0f;

    private float lastDamageTime;
    public bool IsInvincible => Time.realtimeSinceStartup - lastDamageTime < invinciblePeriod;

    public void Heal(float amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;
        OnHeal?.Invoke(maxHealth, health);
    }
    public void Revive()
    {
        health = maxHealth;
        isDead = false;
        OnHeal?.Invoke(maxHealth, health);
    }
    public void TakeDamage(float amount, Vector2 damageDirection)
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
        if (health <= 0 && isDead == false)
        {
            Kill(damageDirection);
        }
    }

    public void Kill(Vector2 damageDirection)
    {
        isDead = true;
        OnDead?.Invoke(damageDirection);
    }
}
