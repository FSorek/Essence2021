using UnityEngine;

public class Player : MonoBehaviour
{
    public WorldPointer WorldPointer => worldPointer;
    [SerializeField] private WorldPointer worldPointer;
}