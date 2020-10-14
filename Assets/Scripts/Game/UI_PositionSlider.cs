using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PositionSlider : MonoBehaviour
{
    [SerializeField] private WorldPosition trackedObject;
    private WorldGenerator worldGenerator;
    private Slider slider;
    
    private void Start()
    {
        worldGenerator = WorldSettings.WorldGenerator;
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.value = trackedObject.GlobalPosition / worldGenerator.MapLength;
    }
}
