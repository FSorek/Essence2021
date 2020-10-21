using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform WorldPointer => worldPointer;
    [SerializeField]private Transform worldPointer;
}