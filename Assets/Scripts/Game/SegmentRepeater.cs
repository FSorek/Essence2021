using System;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class SegmentRepeater : MonoBehaviour
{
    [SerializeField] private WorldPosition trackedObject;
    private ObservableCollection<WorldSegment> segments;
    private int currentSegmentIndex;
    private WorldGenerator worldGenerator;
    private void Start()
    {
        worldGenerator = WorldSettings.WorldGenerator;
        segments = new ObservableCollection<WorldSegment>(worldGenerator.Segments);
    }

    private void Update()
    {
        var percentagePositionOnSegment = 1 - trackedObject.SegmentPosition / trackedObject.CurrentSegment.Length;
        if (percentagePositionOnSegment > 1f)
            currentSegmentIndex++;
        else if (percentagePositionOnSegment < 0)
            currentSegmentIndex--;
        
        if (currentSegmentIndex == segments.Count - 1)
            MoveFirstSegmentToLast();
        else if(currentSegmentIndex == 0)
            MoveLastSegmentToFirst();
    }

    private void MoveFirstSegmentToLast()
    {
        var firstSegment = segments.First();
        var lastSegment = segments.Last();
        firstSegment.AnchorRight(lastSegment);
        segments.Move(0, segments.Count - 1);
        currentSegmentIndex--;
    }

    private void MoveLastSegmentToFirst()
    {
        var firstSegment = segments.First();
        var lastSegment = segments.Last();
        lastSegment.AnchorLeft(firstSegment);
        segments.Move(segments.Count - 1, 0);
        currentSegmentIndex++;
    }
}