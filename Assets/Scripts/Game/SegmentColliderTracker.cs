public class SegmentColliderTracker
{
    public SegmentCollider ClosestCollider { get; private set; }
    public WorldPosition Position => position;

    private WorldPosition position;

    public SegmentColliderTracker(WorldPosition trackedPosition)
    {
        position = trackedPosition;
    }
    
    public void Tick()
    {
        var currentSegment = position.CurrentSegment;
        ClosestCollider = currentSegment.GetClosestCollider(PlayerInput.Instance.MouseRayHitPoint.z);
    }
}