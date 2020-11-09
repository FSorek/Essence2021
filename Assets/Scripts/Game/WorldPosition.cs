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
    public WorldSegment LeftSegment { get; private set; }
    public WorldSegment RightSegment { get; private set; }
    private WorldGenerator worldGenerator;
    private void OnEnable()
    {
        worldGenerator = WorldSettings.WorldGenerator;
        WorldSettings.OnWorldInitialized += WorldSettingsOnWorldInitialized;
    }

    private void WorldSettingsOnWorldInitialized()
    {
        CurrentSegment = worldGenerator.GetSegmentAt(transform.position.x);
        LeftSegment = worldGenerator.GetPreviousSegment(CurrentSegment);
        RightSegment = worldGenerator.GetNextSegment(CurrentSegment);
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (CurrentSegment == null)
        {
            WorldSettingsOnWorldInitialized();
            return;
        }
        if (SegmentPosition > CurrentSegment.Length)
        {
            OnSegmentChanged?.Invoke(LeftSegment);
            RightSegment = CurrentSegment;
            CurrentSegment = LeftSegment;
            LeftSegment = worldGenerator.GetPreviousSegment(CurrentSegment);
        }
        else if (SegmentPosition < 0)
        {
            OnSegmentChanged?.Invoke(RightSegment);
            LeftSegment = CurrentSegment;
            CurrentSegment = RightSegment;
            RightSegment = worldGenerator.GetNextSegment(CurrentSegment);
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
    WorldSegment LeftSegment { get; }
    WorldSegment RightSegment { get; }
}