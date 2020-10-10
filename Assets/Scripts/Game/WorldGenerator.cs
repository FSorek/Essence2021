using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public WorldSegment CurrentViewingSegment => segments[currentSegmentIndex];
    
    [SerializeField] private int startingSegmentCount;
    [SerializeField] private WorldSegment segmentPrefab;
    private List<WorldSegment> segments;
    private int currentSegmentIndex;

    private void Awake()
    {
        var preSpawnedSegments = FindObjectsOfType<WorldSegment>();
        segments = new List<WorldSegment>(preSpawnedSegments);
        currentSegmentIndex = 0;
    }

    private void Start()
    {
        AlignWorldSpawnedSegments();
        PrespawnSegments();
    }

    private void PrespawnSegments()
    {
        if (segments.Count >= startingSegmentCount) 
            return;
        
        int segmentsToAdd = startingSegmentCount - segments.Count;
        for (int i = 0; i < segmentsToAdd; i++)
            CreateSegment();
    }

    private void AlignWorldSpawnedSegments()
    {
        for (int i = 0; i < segments.Count; i++)
        {
            var segment = segments[i];
            if(i + 1 < segments.Count)
                segments[i+1].AnchorLeft(segment);
        }
    }

    private void CreateSegment()
    {
        var newSegment = Instantiate(segmentPrefab);
        var lastSegment = segments.LastOrDefault();
        if(lastSegment != null)
            newSegment.AnchorRight(lastSegment);
        
        segments.Add(newSegment);
    }

    public void MoveFirstSegmentToLast()
    {
        var firstSegment = segments.First();
        var lastSegment = segments.Last();
        firstSegment.AnchorRight(lastSegment);
    }

    public void MoveLastSegmentToFirst()
    {
        var firstSegment = segments.First();
        var lastSegment = segments.Last();
        lastSegment.AnchorLeft(firstSegment);
    }
}
