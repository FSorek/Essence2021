using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public int TotalSegments => segments.Count;
    public WorldSegment CurrentViewingSegment { get; private set; }
    
    [SerializeField] private int startingSegmentCount;
    [SerializeField] private WorldSegment segmentPrefab;
    private List<WorldSegment> segments;

    private void Awake()
    {
        var preSpawnedSegments = FindObjectsOfType<WorldSegment>();
        segments = new List<WorldSegment>(preSpawnedSegments);
        CurrentViewingSegment = segments.First();
    }

    private void Start()
    {
        if (segments.Count < startingSegmentCount)
        {
            int segmentsToAdd = startingSegmentCount - segments.Count;
            for (int i = 0; i < segmentsToAdd; i++)
                CreateSegment();
        }
    }

    private void CreateSegment()
    {
        var newSegment = Instantiate(segmentPrefab);
        newSegment.Anchor(segments.Last());
        segments.Add(newSegment);
    }
}
