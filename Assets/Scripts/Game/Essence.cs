using System;
using Game;
using UnityEngine;

[RequireComponent(typeof(WorldPosition))]
public class Essence : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float attackDelay;
    [SerializeField] private Projectile projectile;
    private Monster target;
    private IWorldPosition position;
    private TargetFinder<Monster> targetFinder;
    private bool canFireProjectile => target != null && shotTimer <= 0;
    private float shotTimer = 0;

    private void Awake()
    {
        position = GetComponent<IWorldPosition>();
        targetFinder = new TargetFinder<Monster>(position, range, WorldSettings.WorldGenerator, WorldSettings.MonsterFactory);
    }

    private void Update()
    {
        if (shotTimer > 0)
            shotTimer -= Time.deltaTime;
        if(target == null || !target.Health.IsAlive)
            target = targetFinder.GetClosestTarget();
        if(canFireProjectile)
            FireProjectile();
    }

    private void FireProjectile()
    {
        var projectileObject = Instantiate(projectile, transform.position, Quaternion.identity);
        projectileObject.SetTarget(target);
        shotTimer = attackDelay;
    }
}