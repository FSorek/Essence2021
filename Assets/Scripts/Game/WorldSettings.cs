using System;
using UnityEngine;

public class WorldSettings : MonoBehaviour
{
    public static WorldGenerator WorldGenerator { get; } = new WorldGenerator();
    [SerializeField] private WorldSegment segmentPrefab;
    [SerializeField][Min(3)] private int startingSegments = 3;
    private void Start()
    {
        CreateStartingSegments();
    }

    private void CreateStartingSegments()
    {
        for (int i = 0; i < startingSegments; i++)
        {
            WorldGenerator.CreateSegment(segmentPrefab);
        }
    }
}