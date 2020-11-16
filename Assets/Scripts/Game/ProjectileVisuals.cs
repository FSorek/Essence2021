using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class ProjectileVisuals : MonoBehaviour
{
    [SerializeField] private float FadeTime;
    private Projectile projectile;
    private Light[] lights;
    private ParticleSystem[] particleSystems;
    private bool dimLights;
    private float fadeStart;
    private float fadeEnd => fadeStart + FadeTime;

    private void Awake()
    {
        lights = GetComponentsInChildren<Light>();
        projectile = GetComponentInParent<Projectile>();
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        
        projectile.OnTargetHit += ProjectileOnTargetHit;
    }

    private void ProjectileOnTargetHit(IEntity obj)
    {
        foreach (var system in particleSystems)
            system.Stop();
        fadeStart = Time.time;
        dimLights = true;
    }

    private void Update()
    {
        if (dimLights)
        {
            foreach (var lightComponent in lights)
            {
                lightComponent.intensity = 1 - Mathf.InverseLerp(fadeStart, fadeEnd, Time.time);
            }
        }
    }

    private void OnEnable()
    {
        dimLights = false;
        foreach (var lightComponent in lights)
        {
            lightComponent.intensity = 1;
        }
    }
}
