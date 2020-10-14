using UnityEngine;

public class WorldSettings : MonoBehaviour
{
    public static WorldGenerator WorldGenerator { get; private set; }
    [SerializeField] private WorldSegment segmentPrefab;
    [SerializeField][Min(3)] private int startingSegments = 3;
    private void Awake()
    {
        if(WorldGenerator == null)
            WorldGenerator = new WorldGenerator();
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