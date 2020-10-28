using System;
using UnityEngine;

[RequireComponent(typeof(WorldPosition))]
public class Essence : MonoBehaviour
{
    [SerializeField] private float range;
    private IWorldPosition target;
    private IWorldPosition position;
    private TargetFinder targetFinder;
    private void Awake()
    {
        position = GetComponent<IWorldPosition>();
        targetFinder = new TargetFinder(position, range, WorldSettings.WorldGenerator);
    }

    private void Update()
    {
        targetFinder.GetClosestTarget(Monster.ActiveMonsters);
    }
}