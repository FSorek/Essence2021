using System;
using UnityEngine;

public class Player : MonoBehaviour, IEssenceHolder
{
    public WorldPointer WorldPointer => worldPointer;
    public LineRenderer BuildLine => buildLine;
    public LineRenderer ExtractLine => extractLine;
    public LineRenderer AbsorbLine => absorbLine;
    public Essence CurrentEssence { get; private set; }
    public SegmentColliderTracker ColliderTracker { get; private set; }
    public ClosestObeliskFinder ObeliskFinder { get; private set; }

    [SerializeField] private WorldPointer worldPointer;
    [SerializeField] private LineRenderer buildLine;
    [SerializeField] private LineRenderer extractLine;
    [SerializeField] private LineRenderer absorbLine;

    private void Start()
    {
        ColliderTracker = new SegmentColliderTracker(PlayerInput.Instance.WorldPointerPosition);
        ObeliskFinder = new ClosestObeliskFinder(.2f);
    }

    private void Update()
    {
        ObeliskFinder.UpdateTarget(worldPointer.transform.position, 1f);
        ColliderTracker.Tick();
    }

    public void AddEssence(Essence essence)
    {
        CurrentEssence = essence;
    }
    public Essence ExtractEssence()
    {
        var extractedEssence = CurrentEssence;
        CurrentEssence = null;
        return extractedEssence;
    }
}