public class Health
{
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }
    public bool IsAlive { get; private set; }

    public Health(float maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        IsAlive = true;
    }
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
            IsAlive = false;
    }
}