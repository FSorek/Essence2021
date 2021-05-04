using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerVisuals : MonoBehaviour
{
    private BuildingVFX[] buildingVfxs;
    private InvokeElementVFX[] invokeElementVfxs;
    private ExtractVFX extractVfx;
    private ExudeVFX exudeVfx;
    [SerializeField] private VisualEffect holdingEssence;
    [SerializeField] private VisualEffect placingObeliskVfx;

    private void Awake()
    {
        var playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        var player = FindObjectOfType<Player>();
        buildingVfxs = GetComponentsInChildren<BuildingVFX>();
        invokeElementVfxs = GetComponentsInChildren<InvokeElementVFX>();
        extractVfx = GetComponentInChildren<ExtractVFX>();
        exudeVfx = GetComponentInChildren<ExudeVFX>();
        playerStateMachine.OnStateEntered += PlayEffect;
        playerStateMachine.OnStateExited += StopEffect;
        player.OnEssenceExtracted += PlayHoldingEssenceEffect;
        player.OnEssenceLost += StopHoldingEssenceEffect;
    }

    private void StopHoldingEssenceEffect()
    {
        holdingEssence.Stop();
    }

    private void PlayHoldingEssenceEffect()
    {
        holdingEssence.Play();
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
        else if (state is Infuse exude)
        {
            exudeVfx?.Play(exude.TargetPosition);
        }
        else if (state is PlacingObelisk)
        {
            placingObeliskVfx.Play();
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
        else if (state is Infuse)
        {
            exudeVfx?.Stop();
        }
        else if (state is PlacingObelisk)
        {
            placingObeliskVfx.Stop();
        }
    }
}