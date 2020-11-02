using System;
using UnityEngine;

public class FireEssenceAttack : MonoBehaviour
{
    private MouseOverSelector enemySelector;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private float tickFrequency;
    [SerializeField] private float damage;
    [SerializeField] private float radius;
    [SerializeField] private int targetCap;
    private float tickTimer;
    private ParticleSystem[] childVisuals;

    private void Awake()
    {
        enemySelector = new MouseOverSelector(targetMask, radius, targetCap);
        childVisuals = GetComponentsInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        tickTimer = tickFrequency;
        foreach (var visual in childVisuals)
            visual.Play();
    }

    private void OnDisable()
    {
        foreach (var visual in childVisuals)
            visual.Stop();
    }

    public void Update()
    {
        if (tickTimer > 0)
        {
            tickTimer -= Time.deltaTime;
            return;
        }

        var enemies = enemySelector.GetAllTargets();
        for(int i = 0; i < enemySelector.TargetsSize; i++)
        {
            var enemy = enemies[i];
            var damageable = enemy.GetComponent<ITakeDamage>();
            if(damageable == null)
                continue;
            
            damageable.Health.TakeDamage(damage);
        }

        tickTimer = tickFrequency;
    }
}