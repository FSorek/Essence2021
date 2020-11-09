using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class WorldSettings : MonoBehaviour
{
    public static event Action OnWorldInitialized;
    public static WorldGenerator WorldGenerator { get; } = new WorldGenerator();
    public static MonsterFactory MonsterFactory { get; } = new MonsterFactory();

    public static WorldPointer ActivePointer => activePointer;
    public static EssenceFactory EssenceFactory => essenceFactory;

    [SerializeField] private WorldSegment[] segmentPrefabs;
    [SerializeField][Min(3)] private int startingSegments = 3;
    [SerializeField] private EssenceSet[] essenceSets;
    private static WorldPointer activePointer;
    private static EssenceFactory essenceFactory;

    private void Awake()
    {
        activePointer = FindObjectOfType<WorldPointer>();
        essenceFactory = new EssenceFactory(essenceSets, transform);
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
            var randomSegmentPrefab = GetRandomSegment();
            var segment = WorldGenerator.CreateSegment(randomSegmentPrefab);
            segment.transform.SetParent(transform);
        }
        OnWorldInitialized?.Invoke();
    }

    private WorldSegment GetRandomSegment()
    {
        int randomIndex = Random.Range(0, segmentPrefabs.Length - 1);
        return segmentPrefabs[randomIndex];
    }
}