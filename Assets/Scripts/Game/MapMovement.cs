using System;
using UnityEngine;

[RequireComponent(typeof(WorldPosition))]
public class MapMovement : MonoBehaviour
{
    public float SpeedMultiplier { get; set; } = 1;
    [SerializeField] private float speed;
    private WorldPosition worldPosition;
    private void Awake()
    {
        worldPosition = GetComponent<WorldPosition>();
        worldPosition.OnSegmentChanged += (segment) => transform.SetParent(segment.transform);
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.left * SpeedMultiplier, Space.Self);
    }
}