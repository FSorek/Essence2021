﻿using System;
using System.Linq;
using UnityEngine;

public class WorldPosition : MonoBehaviour
{
    public event Action<WorldSegment> OnSegmentChanged;
    public float GlobalPosition { get; private set; }
    public float SegmentPosition { get; private set; }
    public WorldSegment CurrentSegment { get; private set; }
    private WorldGenerator worldGenerator;
    private void Start()
    {
        worldGenerator = WorldSettings.WorldGenerator;
        CurrentSegment = worldGenerator.GetSegmentAt(transform.position.x);
    }

    private void Update()
    {
        if (CurrentSegment == null)
        {
            CurrentSegment = worldGenerator.GetSegmentAt(transform.position.x);
            return;
        }
        if (SegmentPosition > CurrentSegment.Length)
        {
            CurrentSegment = worldGenerator.GetPreviousSegment(CurrentSegment);
            OnSegmentChanged?.Invoke(CurrentSegment);
        }
        else if (SegmentPosition < 0)
        {
            CurrentSegment = worldGenerator.GetNextSegment(CurrentSegment);
            OnSegmentChanged?.Invoke(CurrentSegment);
        }
        
        SegmentPosition = transform.InverseTransformPoint(CurrentSegment.transform.position).x;
        GlobalPosition = SegmentPosition + worldGenerator.GetRemainingSegmentsLength(CurrentSegment);
    }
}