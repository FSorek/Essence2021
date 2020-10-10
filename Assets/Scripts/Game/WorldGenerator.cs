using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public WorldSegment CurrentViewingSegment => segments[playerSegmentIndex];
    
    [SerializeField][Min(3)] private int startingSegmentCount = 3;
    [SerializeField] private WorldSegment segmentPrefab;
    private ObservableCollection<WorldSegment> segments;
    private int playerSegmentIndex;
    private Transform player;
    private float percentagePositionOnSegment;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        var preSpawnedSegments = FindObjectsOfType<WorldSegment>();
        segments = new ObservableCollection<WorldSegment>(preSpawnedSegments);
    }

    private void Start()
    {
        AlignWorldSpawnedSegments();
        SpawnSegments();
        playerSegmentIndex = 0;
    }

    private void Update()
    {
        if (playerSegmentIndex == segments.Count - 1)
            MoveFirstSegmentToLast();
        else if(playerSegmentIndex == 0)
            MoveLastSegmentToFirst();
        
        percentagePositionOnSegment = 1 - player.InverseTransformPoint(CurrentViewingSegment.transform.position).x / CurrentViewingSegment.Length;
        if (percentagePositionOnSegment > 1.1f)
            playerSegmentIndex++;
        else if (percentagePositionOnSegment < -.1f)
            playerSegmentIndex--;

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

    private void SpawnSegments()
    {
        if (segments.Count >= startingSegmentCount) 
            return;
        
        int segmentsToAdd = startingSegmentCount - segments.Count;
        for (int i = 0; i < segmentsToAdd; i++)
            CreateSegment();
    }

    private void CreateSegment()
    {
        var newSegment = Instantiate(segmentPrefab);
        var lastSegment = segments.LastOrDefault();
        if(lastSegment != null)
            newSegment.AnchorRight(lastSegment);
        
        segments.Add(newSegment);
    }

    private void MoveFirstSegmentToLast()
    {
        var firstSegment = segments.First();
        var lastSegment = segments.Last();
        firstSegment.AnchorRight(lastSegment);
        segments.Move(0, segments.Count - 1);
        playerSegmentIndex--;
    }

    private void MoveLastSegmentToFirst()
    {
        var firstSegment = segments.First();
        var lastSegment = segments.Last();
        lastSegment.AnchorLeft(firstSegment);
        segments.Move(segments.Count - 1, 0);
        playerSegmentIndex++;
    }
}
