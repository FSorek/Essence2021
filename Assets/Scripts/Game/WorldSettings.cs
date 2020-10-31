using System;
using UnityEngine;

public class WorldSettings : MonoBehaviour
{
    public static WorldGenerator WorldGenerator { get; } = new WorldGenerator();
    public static MonsterFactory MonsterFactory { get; } = new MonsterFactory();
    [SerializeField] private WorldSegment segmentPrefab;
    [SerializeField][Min(3)] private int startingSegments = 3;

    private void OnEnable()
    {
        var prespawnedSegments = FindObjectsOfType<WorldSegment>();
        WorldGenerator.AddSegments(prespawnedSegments);
    }

    private void Start()
    {
        CreateStartingSegments();
    }

    private void CreateStartingSegments()
    {
        for (int i = 0; i < startingSegments; i++)
        {
            var segment = WorldGenerator.CreateSegment(segmentPrefab);
            segment.transform.SetParent(transform);
        }
    }
}