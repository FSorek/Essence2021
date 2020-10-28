using System;
using System.Linq;
using UnityEngine;

public class WorldPosition : MonoBehaviour, IWorldPosition
{
    public event Action<WorldSegment> OnSegmentChanged;
    public Vector3 TruePosition => transform.position;
    public float GlobalPosition { get; private set; }
    public float SegmentPosition { get; private set; }
    public WorldSegment CurrentSegment { get; private set; }
    private WorldGenerator worldGenerator;
    private void OnEnable()
    {
        worldGenerator = WorldSettings.WorldGenerator;
        CurrentSegment = worldGenerator.GetSegmentAt(transform.position.x);
        UpdatePosition();
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (CurrentSegment == null)
        {
            CurrentSegment = worldGenerator.GetSegmentAt(transform.position.x);
            return;
        }

        if (SegmentPosition > CurrentSegment.Length)
        {
            var previousSegment = worldGenerator.GetPreviousSegment(CurrentSegment);
            OnSegmentChanged?.Invoke(previousSegment);
            CurrentSegment = previousSegment;
        }
        else if (SegmentPosition < 0)
        {
            var nextSegment = worldGenerator.GetNextSegment(CurrentSegment);
            OnSegmentChanged?.Invoke(nextSegment);
            CurrentSegment = nextSegment;
        }

        SegmentPosition = transform.InverseTransformPoint(CurrentSegment.transform.position).x;
        GlobalPosition = SegmentPosition + worldGenerator.GetRemainingSegmentsLength(CurrentSegment);
    }
}

public interface IWorldPosition
{
    Vector3 TruePosition { get; }
    float GlobalPosition { get;  }
    float SegmentPosition { get;  }
    WorldSegment CurrentSegment { get;  }

}