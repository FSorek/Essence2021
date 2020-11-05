using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    private BuildingVFX[] buildingVfxs;
    private InvokeElementVFX[] invokeElementVfxs;
    private ExtractVFX extractVfx;

    private void Awake()
    {
        var playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        buildingVfxs = GetComponentsInChildren<BuildingVFX>();
        invokeElementVfxs = GetComponentsInChildren<InvokeElementVFX>();
        extractVfx = GetComponentInChildren<ExtractVFX>();
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
            invokeElementVfxs.FirstOrDefault(vfx => vfx.TargetName == invoke.TargetElement)?.Play();
        }
        else if (state is Extract extract)
        {
            extractVfx?.Play(extract.TargetPosition);
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
            invokeElementVfxs.FirstOrDefault(vfx => vfx.TargetName == invoke.TargetElement)?.Stop();
        }
        else if (state is Extract)
        {
            extractVfx?.Stop();
        }
    }
}