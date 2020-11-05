using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    public Vector3 ParticleOrigin => particleOrigin.position;

    [SerializeField] private Transform particleOrigin;
    [SerializeField] private BuildingVFX[] buildingVfxs;
    [SerializeField] private InvokeElementVFX[] invokeElementVfxes;

    private void Awake()
    {
        var playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        playerStateMachine.OnStateEntered += PlayEffect;
        playerStateMachine.OnStateExited += StopEffect;
    }

    private void PlayEffect(IState state)
    {
        if (state is Building building)
        {
            buildingVfxs.First(vfx => vfx.TargetName == building.Essence).Play(building.TargetPosition);
        }
        else if(state is InvokeElement invoke)
        {
            invokeElementVfxes.First(vfx => vfx.TargetName == invoke.TargetElement).Play();
        }
    }

    private void StopEffect(IState state)
    {
        if (state is Building building)
        {
            buildingVfxs.First(vfx => vfx.TargetName == building.Essence).Stop();
        }
        else if(state is InvokeElement invoke)
        {
            invokeElementVfxes.First(vfx => vfx.TargetName == invoke.TargetElement).Stop();
        }
    }
}