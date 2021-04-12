using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    public float Health => CurrentHealth;
    public float MaxHealth => TotalHealth;

    private float CurrentHealth;
    [SerializeField] private float TotalHealth;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        CurrentHealth = TotalHealth;    
    }

    internal void HealPlayer(int effect)
    {
        if (CurrentHealth < MaxHealth && CurrentHealth > 0)
        {
            CurrentHealth += effect;
        }

        if (CurrentHealth <= 0)
        {
            Destroy();
        }
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    public virtual void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Destroy();
        }
    }

    internal void SetCurrentHealth(float health)
    {
        CurrentHealth = health;
    }
}
