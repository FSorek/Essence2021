using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class WorldSettings : MonoBehaviour
{
    public static WorldGenerator WorldGenerator { get; } = new WorldGenerator();
    public static MonsterFactory MonsterFactory { get; } = new MonsterFactory();

    public static WorldPointer ActivePointer => activePointer;
    [SerializeField] private WorldSegment segmentPrefab;
    [SerializeField][Min(3)] private int startingSegments = 3;
    private static WorldPointer activePointer;

    private void Awake()
    {
        activePointer = FindObjectOfType<WorldPointer>();
    }

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