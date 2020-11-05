using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    private BuildingVFX[] buildingVfxs;
    private InvokeElementVFX[] invokeElementVfxes;

    private void Awake()
    {
        var playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        buildingVfxs = GetComponentsInChildren<BuildingVFX>();
        invokeElementVfxes = GetComponentsInChildren<InvokeElementVFX>();
        playerStateMachine.OnStateEntered += PlayEffect;
        playerStateMachine.OnStateExited += StopEffect;
    }

    private void PlayEffect(IState state)
    {
        if (state is Building building)
        {
            buildingVfxs.FirstOrDefault(vfx => vfx.TargetName == building.Essence)?.Play(building.TargetPosition);
        }
        else if(state is InvokeElement invoke)
        {
            invokeElementVfxes.FirstOrDefault(vfx => vfx.TargetName == invoke.TargetElement)?.Play();
        }
    }

    private void StopEffect(IState state)
    {
        if (state is Building building)
        {
            buildingVfxs.FirstOrDefault(vfx => vfx.TargetName == building.Essence)?.Stop();
        }
        else if(state is InvokeElement invoke)
        {
            invokeElementVfxes.FirstOrDefault(vfx => vfx.TargetName == invoke.TargetElement)?.Stop();
        }
    }
}