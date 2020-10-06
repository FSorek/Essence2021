using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSegment : MonoBehaviour
{
    public float Length { get; private set; }

    private void Awake()
    {
        Length = GetComponentInChildren<MeshRenderer>().bounds.size.x;
    }

    public void Anchor(WorldSegment anchoringSegment)
    {
        var anchorX = anchoringSegment.transform.position.x + Length;
        transform.position = new Vector3(anchorX, 0, 0);
    }
}
