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
        return Physics.OverlapCapsule(WorldSettings.ActivePointer.transform.position, PlayerInput.Instance.MouseRayHitPoint, rayRadius, layerMask);
    }
}