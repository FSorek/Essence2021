using System.Collections.Generic;
using UnityEngine;

public class TargetFinder<T> where T : class, IEntity
{
    private readonly IWorldPosition origin;
    private readonly float range;
    private readonly IWorldGenerator worldGenerator;
    private readonly IEntityFactory<T> factory;
    private WorldPosition[] aliveMonsters;

    public TargetFinder(IWorldPosition origin, float range, IWorldGenerator worldGenerator, IEntityFactory<T> factory)
    {
        this.origin = origin;
        this.range = range;
        this.worldGenerator = worldGenerator;
        this.factory = factory;
    }

    public T GetClosestTarget()
    {
        T closestPosition = null;
        float distance = Mathf.Infinity;
        foreach (var entity in factory.GetAliveEntities())
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

public interface IEntityFactory<T> where T : class, IEntity
{
    IEnumerable<T> GetAliveEntities();
}