using System;
using UnityEngine;
using UnityEngine.VFX;

public class BuildingVFX : MonoBehaviour
{
    public EssenceNames TargetName => targetName;
    [SerializeField] private EssenceNames targetName;
    [SerializeField] private Transform target;
    private VisualEffect[] visualEffects;

    private void Awake()
    {
        visualEffects = GetComponentsInChildren<VisualEffect>();
    }

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