using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    public Vector3 ParticleOrigin => particleOrigin.position;

    [SerializeField] private Transform particleOrigin;
    [SerializeField] private BuildingVFX[] buildingVfxs;

    public void AssignBuildingState(Building state)
    {
        foreach (var vfx in buildingVfxs)
        {
            if(vfx.TargetName == state.Essence)
                vfx.AssignBuildingState(state);
        }
    }
}