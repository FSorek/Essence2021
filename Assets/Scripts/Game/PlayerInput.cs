using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    public static IPlayerInput Instance { get; set; }
    public float MovementInput { get; private set; }
    public bool PrimaryActionKeyDown { get; private set; }
    public bool PrimaryActionKeyUp { get; private set; }
    public bool ObeliskKeyDown { get; private set; }
    public bool InvokeFireDown { get; private set; }
    public bool InvokeWaterDown { get; private set; }
    public bool InvokeEarthDown { get; private set; }
    public bool InvokeAirDown { get; private set; }

    public void UpdateMovement(InputAction.CallbackContext context) => MovementInput = context.ReadValue<float>();
    public void UpdateObeliskKeyDown(InputAction.CallbackContext context) => ObeliskKeyDown = context.performed;
    public void UpdateInvokeFireDown(InputAction.CallbackContext context) => InvokeFireDown = context.performed;
    public void UpdateInvokeWaterDown(InputAction.CallbackContext context) => InvokeWaterDown = context.performed;
    public void UpdateInvokeEarthDown(InputAction.CallbackContext context) => InvokeEarthDown = context.performed;
    public void UpdateInvokeAirDown(InputAction.CallbackContext context) => InvokeAirDown = context.performed;
    public void UpdatePrimaryActionKeyDown(InputAction.CallbackContext context) => PrimaryActionKeyDown = context.started;
    public void UpdatePrimaryActionKeyUp(InputAction.CallbackContext context) => PrimaryActionKeyUp = context.canceled;
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
    bool PrimaryActionKeyDown { get; }
    bool PrimaryActionKeyUp { get; }
    bool ObeliskKeyDown { get; }
    bool InvokeFireDown { get; }
    bool InvokeWaterDown { get; }
    bool InvokeEarthDown { get; }
    bool InvokeAirDown { get; }
}