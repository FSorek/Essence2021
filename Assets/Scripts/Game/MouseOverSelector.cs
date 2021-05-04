using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class MouseOverSelector
{
    private readonly ISelectionStrategy selectionStrategy;
    private readonly int targetCap;
    private Collider[] targets;
    private Func<Collider, bool> filter;

    public MouseOverSelector(ISelectionStrategy selectionStrategy, int targetCap, Func<Collider, bool> defaultFilter = null)
    {
        this.selectionStrategy = selectionStrategy;
        this.targetCap = targetCap;
        filter = defaultFilter;
    }

    public Collider[] GetAllTargets()
    {
        targets = selectionStrategy.GetTargets().Take(targetCap).ToArray();
        return targets;
    }
    public Collider GetTarget()
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