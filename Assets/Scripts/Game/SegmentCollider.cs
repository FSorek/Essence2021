using System.Collections.Generic;
using UnityEngine;

public class SegmentCollider : MonoBehaviour
{
    public BoxCollider Collider { get; private set; }

    private void Awake()
    {
        Collider = GetComponent<BoxCollider>();
    }
}