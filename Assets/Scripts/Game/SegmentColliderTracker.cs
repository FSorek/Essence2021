public class SegmentColliderTracker
{
    public SegmentCollider ClosestCollider { get; private set; }
    public IWorldPosition Position => position;

    private IWorldPosition position;

    public SegmentColliderTracker(IWorldPosition trackedPosition)
    {
        position = trackedPosition;
    }
    
    public void Tick()
    {
        var currentSegment = position.CurrentSegment;
        if(currentSegment == null)
            return;
        ClosestCollider = currentSegment.GetClosestCollider(PlayerInput.Instance.MouseRayHitPoint.z);
    }
}