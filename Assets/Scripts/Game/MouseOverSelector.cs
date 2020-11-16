using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class MouseOverSelector
{
    public int TargetsSize { get; private set; }
    private readonly float rayRadius;
    private readonly LayerMask layerMask;
    private readonly Collider[] targets;

    public MouseOverSelector(LayerMask layerMask, float radius, int targetCap)
    {
        this.layerMask = layerMask;
        targets = new Collider[targetCap];
        rayRadius = radius;
    }

    public Collider[] GetAllTargets()
    {
        TargetsSize = Physics.OverlapCapsuleNonAlloc(WorldSettings.ActivePointer.transform.position, PlayerInput.Instance.MouseRayHitPoint, rayRadius, targets, layerMask);
        return targets;
    }
    public Collider GetTarget(Func<Collider, bool> filter = null)
    {
        TargetsSize = Physics.OverlapCapsuleNonAlloc(WorldSettings.ActivePointer.transform.position, PlayerInput.Instance.MouseRayHitPoint, rayRadius, targets, layerMask);
        if (TargetsSize <= 0)
            return null;
        Collider target = filter != null ? targets.FirstOrDefault(filter.Invoke) : targets.FirstOrDefault();
        return target;
    }
}