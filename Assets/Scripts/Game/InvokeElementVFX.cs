using System;
using UnityEngine;
using UnityEngine.VFX;

public class InvokeElementVFX : MonoBehaviour
{
    public EssenceNames TargetName => targetName;
    [SerializeField] private EssenceNames targetName;
    private VisualEffect[] visualEffects;

    private void Awake()
    {
        visualEffects = GetComponentsInChildren<VisualEffect>();
    }

    public void Play()
    {
        foreach (var effect in visualEffects)
        {
            effect.Play();
        }
    }

    public void Stop()
    {
        foreach (var effect in visualEffects)
        {
            effect.Stop();
        }
    }
}