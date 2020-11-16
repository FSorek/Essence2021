using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 1;
    private CinemachineVirtualCamera camera;
    private CinemachineTrackedDolly track;
    private void Awake()
    {
        camera = GetComponent<CinemachineVirtualCamera>();
        track = camera.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    private void Update()
    {
        if(PlayerInput.Instance.CameraZoomInput != 0)
            track.m_PathPosition = Mathf.Clamp(track.m_PathPosition + Time.deltaTime * zoomSpeed * PlayerInput.Instance.CameraZoomInput, 0, 2);
    }
}
