using System;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;

public class WorldGenerator
{
    public event Action<WorldSegment> OnSegmentCreated;
    public float MapLength => segments.Sum(segment => segment.Length);
    private readonly List<WorldSegment> segments = new List<WorldSegment>();
    
    public WorldSegment CreateSegment(WorldSegment prefab)
    {
        var newSegment = Object.Instantiate(prefab);
        var lastSegment = segments.LastOrDefault();
        if(lastSegment != null)
            newSegment.AnchorRight(lastSegment);
        
        segments.Add(newSegment);
        OnSegmentCreated?.Invoke(newSegment);
        return newSegment;
    }

    public float GetRemainingSegmentsLength(WorldSegment segment)
    {
        float length = 0;
        var currentPlayerSegment = segments.IndexOf(segment);
        for (int i = currentPlayerSegment + 1; i < segments.Count; i++)
        {
            length += segments[i].Length;
        }

        return length;
    }

    public WorldSegment GetPreviousSegment(WorldSegment segment)
    {
        var previousIndex = segments.IndexOf(segment) - 1;
        if (previousIndex < 0)
            return segments.Last();
        return segments[previousIndex];
    }

    public WorldSegment GetNextSegment(WorldSegment segment)
    {
        var nextIndex = segments.IndexOf(segment) + 1;
        if (nextIndex >= segments.Count)
            return segments.First();
        return segments[nextIndex];
    }

    public WorldSegment GetSegmentAt(float positionX)
    {
        foreach (var segment in segments)
        {
            var segmentPosX = segment.transform.position.x;
            if (positionX < segmentPosX && positionX > segmentPosX - segment.Length)
                return segment;
        }

        return null;
    }
}