﻿using System;
using UnityEngine;
using UnityEngine.VFX;

public class BuildingVFX : MonoBehaviour
{
    public EssenceNames TargetName { get; }
    [SerializeField] private EssenceNames targetName;
    [SerializeField] private VisualEffect[] visualEffects;
    [SerializeField] private Transform target;

    public void Play(Vector3 targetPosition)
    {
        target.SetParent(null);
        target.transform.position = targetPosition;
        foreach (var effect in visualEffects)
        {
            effect.Play();
        }
    }

    public void Stop()
    {
        target.SetParent(transform);
        foreach (var effect in visualEffects)
        {
            effect.Stop();
        }
    }
}