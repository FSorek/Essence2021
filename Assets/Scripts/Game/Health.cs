using System;
using UnityEditor;

[System.Serializable]
public class Health
{
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }
    public bool IsAlive { get; private set; }
    public event Action OnTakeDamage;

    public Health(float maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        IsAlive = true;
    }
    public void TakeDamage(float damage)
    {
        if(!IsAlive) return;
        
        CurrentHealth -= damage;
        OnTakeDamage?.Invoke();
        if (CurrentHealth <= 0)
            IsAlive = false;
    }
}