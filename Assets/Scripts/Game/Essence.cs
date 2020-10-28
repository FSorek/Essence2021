using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Essence : MonoBehaviour
{
    
}

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

    public IWorldPosition GetTarget(IWorldPosition[] availableTargets)
    {
        IWorldPosition closestPosition = null;
        float distance = Mathf.Infinity;
        foreach (var monster in availableTargets)
        {
            var monsterDistance = Mathf.Abs(monster.GlobalPosition - origin.GlobalPosition);
            var reapeatDistance = Mathf.Abs(monster.GlobalPosition + worldGenerator.MapLength - origin.GlobalPosition);
            monsterDistance = monsterDistance < reapeatDistance ? monsterDistance : reapeatDistance;
            if(monsterDistance > range || distance < monsterDistance)
                continue;
            
            distance = monsterDistance;
            closestPosition = monster;
        }
        return closestPosition;
    }
}