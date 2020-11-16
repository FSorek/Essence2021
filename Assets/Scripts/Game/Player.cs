using System;
using UnityEngine;

public class Player : MonoBehaviour, IEssenceHolder
{
    public event Action OnEssenceExtracted;
    public event Action OnEssenceLost;
    public WorldPointer WorldPointer => worldPointer;
    public PlayerVisuals Visuals { get; private set; }
    public Essence CurrentEssence { get; private set; }
    public SegmentColliderTracker ColliderTracker { get; private set; }
    [SerializeField] private WorldPointer worldPointer;

    private void Awake()
    {
        Visuals = GetComponentInChildren<PlayerVisuals>();
    }

    private void Start()
    {
        ColliderTracker = new SegmentColliderTracker(PlayerInput.Instance.WorldPointerPosition);
    }

    private void Update()
    {
        ColliderTracker.Tick();
    }

    public void AddEssence(Essence essence)
    {
        CurrentEssence = essence;
        OnEssenceExtracted?.Invoke();
    }
    public Essence ExtractEssence()
    {
        var extractedEssence = CurrentEssence;
        CurrentEssence = null;
        OnEssenceLost?.Invoke();
        return extractedEssence;
    }
}