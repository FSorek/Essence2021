using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSegment : MonoBehaviour
{
    public float Length => modelMeshRenderer.bounds.size.x;
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
}
