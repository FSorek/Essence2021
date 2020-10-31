using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WorldPosition))]
public class Monster : MonoBehaviour, ITakeDamage, IEntity
{
    public static List<Monster> ActiveMonsters { get; } = new List<Monster>();
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

    private void OnEnable()
    {
        ActiveMonsters.Add(this);
    }

    private void OnDisable()
    {
        ActiveMonsters.Remove(this);
    }
}

public interface IEntity
{
    IWorldPosition Position { get; }
}