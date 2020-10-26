using UnityEngine;

[RequireComponent(typeof(WorldPosition))]
public class SegmentColliderTracker : MonoBehaviour
{
    public SegmentCollider ClosestCollider { get; private set; }

    public WorldPosition Position => position;

    private WorldPosition position;

    private void Awake()
    {
        position = GetComponent<WorldPosition>();
    }

    private void Update()
    {
        var currentSegment = position.CurrentSegment;
        ClosestCollider = currentSegment.GetClosestCollider(PlayerInput.Instance.MouseRayHitPoint.z);
    }
}