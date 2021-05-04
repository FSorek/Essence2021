using System.Linq;
using UnityEngine;

public class PhysicsLayerStrategy : ISelectionStrategy
{
    private readonly LayerMask layerMask;
    private readonly float rayRadius;

    public PhysicsLayerStrategy(LayerMask layerMask, float rayRadius)
    {
        this.layerMask = layerMask;
        this.rayRadius = rayRadius;
    }
    public Collider[] GetTargets()
    {
        var targets = Physics.OverlapCapsule(WorldSettings.ActivePointer.transform.position,
            PlayerInput.Instance.MouseRayHitPoint, rayRadius, layerMask);
        
        return targets.OrderBy(c => Vector3.Distance(PlayerInput.Instance.MouseRayHitPoint, c.transform.position)).ToArray();
    }
}