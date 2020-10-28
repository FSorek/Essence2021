using UnityEngine;

[RequireComponent(typeof(WorldPosition))]
public class SegmentMovementAdjustment : MonoBehaviour
{
    private WorldPosition position;
    private float segmentSwitchDeadzone = .2f;

    private void Awake()
    {
        position = GetComponent<WorldPosition>();
        position.OnSegmentChanged += AdjustPosition;
    }

    private void AdjustPosition(WorldSegment segment)
    {
        if (position.SegmentPosition > position.CurrentSegment.Length)
        {
            var adjustedPosition = new Vector3(segment.transform.position.x - segmentSwitchDeadzone, transform.position.y, transform.position.z);
            transform.position = adjustedPosition;
        }
        else if (position.SegmentPosition < 0)
        {
            var adjustedPosition = new Vector3(segment.transform.position.x - segment.Length + segmentSwitchDeadzone, transform.position.y, transform.position.z);
            transform.position = adjustedPosition;
        }
    }
}