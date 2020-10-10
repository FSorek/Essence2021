using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PositionSlider : MonoBehaviour
{
    [SerializeField] private Transform player;
    private WorldGenerator worldGenerator;
    private Slider slider;
    
    private void Start()
    {
        worldGenerator = FindObjectOfType<WorldGenerator>();
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.value = 1 - player.InverseTransformPoint(worldGenerator.CurrentViewingSegment.transform.position).x / worldGenerator.CurrentViewingSegment.Length;
    }
}
