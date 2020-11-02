using System;
using UnityEngine;

[RequireComponent(typeof(WorldPosition))]
public class Monster : MonoBehaviour, ITakeDamage, IEntity
{
    public event Action<Monster> OnDeath;
    public Health Health => health;
    public IWorldPosition Position => position;
    [SerializeField] private float maxHealth;
    private Health health;
    private IWorldPosition position;
    private void Awake()
    {
        position = GetComponent<IWorldPosition>();
        health = new Health(maxHealth);
    }

    private void Update()
    {
        if (!health.IsAlive && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            OnDeath?.Invoke(this);
        }
    }

    public void MultiplyMovementSpeed(float multiplier)
    {
        GetComponent<MapMovement>().SpeedMultiplier = multiplier;
    }
}