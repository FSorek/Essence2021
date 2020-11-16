using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class ProjectileLight : MonoBehaviour
{
    private Light lightComponent;
    private float initialIntensity;
    private void Awake()
    {
        lightComponent = GetComponent<Light>();
    }

    private void DimLight(IEntity obj)
    {
        initialIntensity = lightComponent.intensity;
        StartCoroutine(nameof(Dim));
    }

    private IEnumerator Dim()
    {
        while (lightComponent.intensity > 0)
        {
            lightComponent.intensity -= .1f * Time.deltaTime;
            yield return null;
        }
    }
}
