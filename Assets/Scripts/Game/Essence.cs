using System;
using Game;
using UnityEngine;

[RequireComponent(typeof(WorldPosition))]
public class Essence : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float attackDelay;
    private IWorldPosition target;
    private IWorldPosition position;
    private TargetFinder targetFinder;
    private bool canFireProjectile => target != null && shotTimer <= 0;
    private float shotTimer = 0;

    private void Awake()
    {
        position = GetComponent<IWorldPosition>();
        targetFinder = new TargetFinder(position, range, WorldSettings.WorldGenerator);
    }

    private void Update()
    {
        if (shotTimer > 0)
            shotTimer -= Time.deltaTime;
        if (target != null && Mathf.Abs(target.GlobalPosition - position.GlobalPosition) > range)
            target = null;
        if(target == null)
            target = targetFinder.GetClosestTarget(Monster.ActiveMonsters);

        if(canFireProjectile)
            FireProjectile();
        
    }

    private void FireProjectile()
    {
        var projectile = new GameObject("Projectile").AddComponent<Projectile>();
        projectile.SetTarget(target);
        shotTimer = attackDelay;
    }
}