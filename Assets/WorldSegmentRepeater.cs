using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSegmentRepeater : MonoBehaviour
{
    [SerializeField] private Transform player;
    private WorldGenerator worldGenerator;

    private void Awake()
    {
        worldGenerator = GetComponent<WorldGenerator>();
    }

    private void Update()
    {
        
    }
}
