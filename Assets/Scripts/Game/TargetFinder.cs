using System.Collections.Generic;
using UnityEngine;

public class TargetFinder
{
    private readonly IWorldPosition origin;
    private readonly float range;
    private readonly IWorldGenerator worldGenerator;
    private WorldPosition[] aliveMonsters;

    public TargetFinder(IWorldPosition origin, float range, IWorldGenerator worldGenerator)
    {
        this.origin = origin;
        this.range = range;
        this.worldGenerator = worldGenerator;
    }

    public T GetClosestTarget<T>(IEnumerable<T> availableTargets) where T : class, IEntity
    {
        T closestPosition = null;
        float distance = Mathf.Infinity;
        foreach (var entity in availableTargets)
        {
            var monsterDistance = Mathf.Abs(entity.Position.GlobalPosition - origin.GlobalPosition);
            var reapeatDistance = Mathf.Abs(entity.Position.GlobalPosition + worldGenerator.MapLength - origin.GlobalPosition);
            monsterDistance = monsterDistance < reapeatDistance ? monsterDistance : reapeatDistance;
            if(monsterDistance > range || distance < monsterDistance)
                continue;
            
            distance = monsterDistance;
            closestPosition = entity;
        }
        return closestPosition;
    }
}