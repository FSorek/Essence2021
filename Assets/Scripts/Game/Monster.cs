using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WorldPosition))]
public class Monster : MonoBehaviour, ITakeDamage
{
    public static List<IWorldPosition> ActiveMonsters { get; } = new List<IWorldPosition>();
    [SerializeField] private float maxHealth;
    public Health Health { get; private set; }
    private IWorldPosition position;
    private void Awake()
    {
        position = GetComponent<IWorldPosition>();
        Health = new Health(maxHealth);
    }

    private void OnEnable()
    {
        ActiveMonsters.Add(position);
    }

    private void OnDisable()
    {
        ActiveMonsters.Remove(position);
    }
}