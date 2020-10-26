using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public WorldPointer WorldPointer => worldPointer;
    public LineRenderer BuildLine => buildLine;

    [SerializeField] private WorldPointer worldPointer;
    [SerializeField] private LineRenderer buildLine;
    public ClosestObeliskFinder ObeliskFinder { get; private set; }

    private void Awake()
    {
        ObeliskFinder = new ClosestObeliskFinder(.2f);
    }

    private void Update()
    {
        ObeliskFinder.UpdateTarget(worldPointer.transform.position, 1f);
    }
}