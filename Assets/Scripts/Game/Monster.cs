using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WorldPosition))]
public class Monster : MonoBehaviour
{
    public static List<IWorldPosition> ActiveMonsters { get; } = new List<IWorldPosition>();
    private IWorldPosition position;
    private void Awake()
    {
        position = GetComponent<IWorldPosition>();
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
