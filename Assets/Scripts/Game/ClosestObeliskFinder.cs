using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClosestObeliskFinder
{
    public Obelisk Target { get; private set; }
    private readonly float updateFrequency;
    private float updateTimer;
    private int obeliskLayer;

    public ClosestObeliskFinder(float updateFrequency)
    {
        this.updateFrequency = updateFrequency;
        obeliskLayer = LayerMask.GetMask("Obelisk");
    }

    public void UpdateTarget(Vector3 origin, float range)
    {
        updateTimer -= Time.deltaTime;
        if(updateTimer > 0)
            return;

        var obelisksUnderPointer = Physics.OverlapCapsule(origin, PlayerInput.Instance.MouseRayHitPoint, range, obeliskLayer);
        var closestCollider = obelisksUnderPointer.FirstOrDefault();
        if (closestCollider == null)
        {
            if (Target != null)
            {
                Target.Dehighlight();
                Target = null;
            }
            return;
        }
        
        var closestObelisk = closestCollider.GetComponent<Obelisk>();
        if (Target != closestObelisk)
        {
            if(Target != null)
                Target.Dehighlight();
            Target = closestObelisk;
        }

        if (Target != null)
            Target.Highlight();
        
        updateTimer = updateFrequency;
    }
}

public class MouseOverSelector
{
    public IEnumerable<Collider> Targets => targets;
    public int TargetsSize { get; private set; }
    private readonly float rayRadius = 1f;
    private readonly LayerMask layerMask;
    private readonly Transform pointer;
    private Collider[] targets = new Collider[5];

    public MouseOverSelector(LayerMask layerMask, WorldPointer pointer)
    {
        this.layerMask = layerMask;
        this.pointer = pointer.transform;
    }

    public Collider GetTarget(Func<Collider, bool> filter = null)
    {
        TargetsSize = Physics.OverlapCapsuleNonAlloc(pointer.position, PlayerInput.Instance.MouseRayHitPoint, rayRadius, targets, layerMask);
        if (TargetsSize <= 0)
            return null;
        Collider target = filter != null ? targets.FirstOrDefault(filter.Invoke) : targets.FirstOrDefault();
        return target;
    }
}