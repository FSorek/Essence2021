using System;
using UnityEngine;
using UnityEngine.VFX;

public class BuildingVFX : MonoBehaviour
{
    public EssenceNames TargetName { get; }
    [SerializeField] private EssenceNames targetName;
    [SerializeField] private VisualEffect[] visualEffects;
    [SerializeField] private Transform target;
    private Building assignedState;

    private void Awake()
    {
        Stop();
    }

    public void AssignBuildingState(Building state)
    {
        assignedState = state;
        state.OnBuildingStarted += Play;
        state.OnBuildingFinished += Stop;
    }
    private void Play()
    {
        target.position = assignedState.TargetPosition;
        foreach (var effect in visualEffects)
        {
            effect.Play();
        }
    }

    private void Stop()
    {
        foreach (var effect in visualEffects)
        {
            effect.Stop();
        }
    }
}