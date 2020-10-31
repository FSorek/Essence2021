using System;
using Game;
using UnityEngine;

[RequireComponent(typeof(WorldPosition))]
public class Essence : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float attackDelay;
    [SerializeField] private Projectile projectile;
    private IWorldPosition position;
    private TargetFinder<Monster> targetFinder;
    private bool CanFireProjectile => targetFinder.CurrentTarget != null && shotTimer <= 0;
    private float shotTimer = 0;

    private void Awake()
    {
        position = GetComponent<IWorldPosition>();
        targetFinder = new TargetFinder<Monster>(position, range, WorldSettings.WorldGenerator, WorldSettings.MonsterFactory);
    }

    private void Update()
    {
        if (shotTimer > 0)
        {
            shotTimer -= Time.deltaTime;
            return;
        }
        targetFinder.UpdateTarget();
        if(CanFireProjectile)
            FireProjectile();
    }

    private void FireProjectile()
    {
        var projectileObject = Instantiate(projectile, transform.position, Quaternion.identity);
        projectileObject.SetTarget(targetFinder.CurrentTarget);
        shotTimer = attackDelay;
    }
}