﻿using UnityEngine;

public class InvokeElement : IState
{
    private readonly MouseOverSelector obeliskSelector;
    public bool CanBuild => obeliskSelector.GetTarget(CanBuildOnTarget) != null;
    public InvokeElement(MouseOverSelector obeliskSelector)
    {
        this.obeliskSelector = obeliskSelector;
    }
    public void Tick()
    {
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    private bool CanBuildOnTarget(Collider collider)
    {
        if (collider == null)
            return false;
        var obelisk = collider.GetComponent<Obelisk>();
        if (obelisk == null)
            return false;

        if (obelisk.CurrentEssence != null)
            return false;
        
        return true;
    }
}