using System;
using UnityEngine;

[RequireComponent(typeof(WorldPosition))]
public class SegmentColliderTracker : MonoBehaviour
{
    public Collider ClosestCollider { get; private set; }
    private WorldPosition position;

    private void Awake()
    {
        position = GetComponent<WorldPosition>();
    }

    private void Update()
    {
        var currentSegment = position.CurrentSegment;
        ClosestCollider = currentSegment.GetClosestCollider(transform.localPosition.z);
    }
}