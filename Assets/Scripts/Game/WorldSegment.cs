using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSegment : MonoBehaviour
{
    public float Length => modelMeshRenderer.bounds.size.x;
    [SerializeField] private WorldSegmentColliders colliders;
    [SerializeField] private float zAxisTopMidPoint;
    [SerializeField] private float zAxisBottomMidPoint;
    private MeshRenderer modelMeshRenderer;
    private void Awake()
    {
        modelMeshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void AnchorRight(WorldSegment anchoringSegment)
    {
        var anchorX = anchoringSegment.transform.position.x + Length;
        transform.position = new Vector3(anchorX, 0, 0);
    }
    public void AnchorLeft(WorldSegment anchoringSegment)
    {
        var anchorX = anchoringSegment.transform.position.x - anchoringSegment.Length;
        transform.position = new Vector3(anchorX, 0, 0);
    }

    public SegmentCollider GetClosestCollider(float zPosition, float offset = 0)
    {
        if (zPosition > 0 + offset)
            return zPosition > zAxisTopMidPoint ? colliders.NorthTop : colliders.NorthBottom;
        return zPosition < zAxisBottomMidPoint ? colliders.SouthTop : colliders.SouthBottom;
    }
}

[Serializable]
public class WorldSegmentColliders
{
    public SegmentCollider NorthTop;
    public SegmentCollider NorthBottom;
    public SegmentCollider SouthTop;
    public SegmentCollider SouthBottom;
}