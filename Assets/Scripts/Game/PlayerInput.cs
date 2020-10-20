using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    public static IPlayerInput Instance { get; set; }
    public float MovementInput { get; private set; }
    public bool ObeliskKeyDown { get; private set; }

    public void UpdateMovement(InputAction.CallbackContext context) => MovementInput = context.ReadValue<float>();
    public void UpdateObeliskKeyDown(InputAction.CallbackContext context) => ObeliskKeyDown = context.performed;

    private void Awake()
    {
        if (Instance != null)
            Instance = this;
        else
        {
            gameObject.SetActive(false);
            Debug.LogWarning("More than one Player Input instance. Script disabled on " + gameObject.name);
        };
    }
}

public interface IPlayerInput
{
    float MovementInput { get; }
    bool ObeliskKeyDown { get; }
}