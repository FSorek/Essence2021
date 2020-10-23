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

    public Collider GetClosestCollider(float zPosition)
    {
        if (zPosition > 3.8)
            return zPosition > zAxisTopMidPoint ? colliders.NorthTop : colliders.NorthBottom;
        return zPosition < zAxisBottomMidPoint ? colliders.SouthTop : colliders.SouthBottom;
    }
}

[Serializable]
public class WorldSegmentColliders
{
    public Collider NorthTop;
    public Collider NorthBottom;
    public Collider SouthTop;
    public Collider SouthBottom;
}