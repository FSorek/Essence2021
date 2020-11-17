using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class MouseOverSelector
{
    private readonly ISelectionStrategy selectionStrategy;
    private readonly int targetCap;
    private Collider[] targets;

    public MouseOverSelector(ISelectionStrategy selectionStrategy, int targetCap)
    {
        this.selectionStrategy = selectionStrategy;
        this.targetCap = targetCap;
    }

    public Collider[] GetAllTargets()
    {
        targets = selectionStrategy.GetTargets().Take(targetCap).ToArray();
        return targets;
    }
    public Collider GetTarget(Func<Collider, bool> filter = null)
    {
        targets = selectionStrategy.GetTargets();
        if (targets.Length <= 0)
            return null;
        Collider target = filter != null ? targets.FirstOrDefault(filter.Invoke) : targets.FirstOrDefault();
        return target;
    }
}

public interface ISelectionStrategy
{
    Collider[] GetTargets();
}