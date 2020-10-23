using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    [SerializeField] private InputAction obeliskAction;
    [SerializeField] private InputAction primaryAction;
    public static IPlayerInput Instance { get; set; }
    public float MovementInput { get; private set; }
    public bool PrimaryActionKeyDown { get; private set; }
    public bool PrimaryActionKeyUp { get; }
    public bool ObeliskKeyDown { get; private set; }
    public bool InvokeFireDown { get; private set; }
    public bool InvokeWaterDown { get; private set; }
    public bool InvokeEarthDown { get; private set; }
    public bool InvokeAirDown { get; private set; }
    public Vector2 PointerMovement { get; private set; }

    public void UpdateMovement(InputAction.CallbackContext context) => MovementInput = context.ReadValue<float>();
    public void UpdateInvokeFireDown(InputAction.CallbackContext context) => InvokeFireDown = context.ReadValueAsButton();
    public void UpdateInvokeWaterDown(InputAction.CallbackContext context) => InvokeWaterDown = context.ReadValueAsButton();
    public void UpdateInvokeEarthDown(InputAction.CallbackContext context) => InvokeEarthDown = context.ReadValueAsButton();
    public void UpdateInvokeAirDown(InputAction.CallbackContext context) => InvokeAirDown = context.ReadValueAsButton();
    public void UpdatePointerMovement(InputAction.CallbackContext context) => PointerMovement = context.ReadValue<Vector2>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            gameObject.SetActive(false);
            Debug.LogWarning("More than one Player Input instance. Script disabled on " + gameObject.name);
        };
    }

    private void Update()
    {
        ObeliskKeyDown = ButtonPressedThisFrame(obeliskAction);
        PrimaryActionKeyDown = ButtonPressedThisFrame(primaryAction);
        Debug.Log(PrimaryActionKeyDown);
    }

    private bool ButtonPressedThisFrame(InputAction action)
    {
        bool pressed = false;
        foreach (var control in action.controls)
        {
            pressed = ((ButtonControl) control).wasPressedThisFrame | pressed;
        }

        return pressed;
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
    Vector2 PointerMovement { get; }
}