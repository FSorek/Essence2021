using UnityEngine;

[RequireComponent(typeof(WorldPosition))]
public class SegmentMovementAdjustment : MonoBehaviour
{
    private WorldPosition position;

    private void Awake()
    {
        position = GetComponent<WorldPosition>();
        position.OnSegmentChanged += AdjustPosition;
    }

    private void AdjustPosition(WorldSegment segment)
    {
        if (position.SegmentPosition > segment.Length)
        {
            var adjustedPosition = new Vector3(segment.transform.position.x, transform.position.y, transform.position.z);
            transform.position = adjustedPosition;
        }
    }
}