using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    [SerializeField] private bool cursorLocked;
    [SerializeField] private InputAction obeliskAction;
    [SerializeField] private InputAction primaryAction;
    [SerializeField] private InputAction secondaryAction;
    [SerializeField] private InputAction invokeFireAction;
    [SerializeField] private InputAction invokeWaterAction;
    [SerializeField] private InputAction invokeEarthAction;
    [SerializeField] private InputAction invokeAirAction;
    [SerializeField] private InputAction attackAction;
    [SerializeField] private InputAction jumpLeftAction;
    [SerializeField] private InputAction jumpRightAction;
    public static IPlayerInput Instance { get; set; }
    public IWorldPosition WorldPointerPosition => playerPointer.Position;
    public float MovementInput { get; private set; }
    public bool PrimaryActionKeyDown { get; private set; }
    public bool PrimaryActionKeyUp { get; private set; }
    public bool ObeliskKeyDown { get; private set; }
    public bool InvokeFireDown { get; private set; }
    public bool InvokeWaterDown { get; private set; }
    public bool InvokeEarthDown { get; private set; }
    public bool InvokeAirDown { get; private set; }
    public Vector2 PointerMovement { get; private set; }
    public Vector3 MouseRayHitPoint { get; private set; } = Vector3.zero;
    public bool SecondaryActionKeyDown { get; private set; }
    public bool SecondaryActionKeyUp { get; private set; }
    public bool AttackActionKeyDown { get; private set; }
    public bool AttackActionKeyUp { get; private set; }
    public bool JumpLeftKeyDown { get; private set; }
    public bool JumpRightKeyDown { get; private set; }

    public void UpdateMovement(InputAction.CallbackContext context) => MovementInput = context.ReadValue<float>();
    public void UpdatePointerMovement(InputAction.CallbackContext context) => PointerMovement = context.ReadValue<Vector2>();
    [SerializeField] private LayerMask mouseRayLayerMask;
    [SerializeField] private WorldPointer playerPointer;
    private Camera playerCamera;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            gameObject.SetActive(false);
            Debug.LogWarning("More than one Player Input instance. Script disabled on " + gameObject.name);
        };
        playerCamera = Camera.main;
        if (playerPointer == null)
            playerPointer = FindObjectOfType<WorldPointer>();

        if (cursorLocked)
            Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        ObeliskKeyDown = ButtonPressedThisFrame(obeliskAction);
        PrimaryActionKeyDown = ButtonPressedThisFrame(primaryAction);
        PrimaryActionKeyUp = ButtonReleasedThisFrame(primaryAction);
        SecondaryActionKeyDown = ButtonPressedThisFrame(secondaryAction);
        SecondaryActionKeyUp = ButtonReleasedThisFrame(secondaryAction);
        InvokeFireDown = ButtonPressedThisFrame(invokeFireAction);
        InvokeWaterDown = ButtonPressedThisFrame(invokeWaterAction);
        InvokeEarthDown = ButtonPressedThisFrame(invokeEarthAction);
        InvokeAirDown = ButtonPressedThisFrame(invokeAirAction);
        AttackActionKeyDown = ButtonPressedThisFrame(attackAction);
        AttackActionKeyUp = ButtonReleasedThisFrame(attackAction);
        MouseRayHitPoint = ReadMouseRay();
        JumpLeftKeyDown = ButtonPressedThisFrame(jumpLeftAction);
        JumpRightKeyDown = ButtonPressedThisFrame(jumpRightAction);
    }

    private Vector3 ReadMouseRay()
    {
        if (playerPointer == null)
            return Vector3.zero;
        Vector3 hitPosition = Vector3.zero;
        var pointerScreenPosition = playerCamera.WorldToScreenPoint(playerPointer.PointingPosition);
        var cameraRay = playerCamera.ScreenPointToRay(pointerScreenPosition);
        if (Physics.Raycast(cameraRay, out var mouseRayHit, Mathf.Infinity, mouseRayLayerMask))
        {
            hitPosition = mouseRayHit.point;
        }
        return hitPosition;
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
    private bool ButtonReleasedThisFrame(InputAction action)
    {
        bool released = false;
        foreach (var control in action.controls)
        {
            released = ((ButtonControl) control).wasReleasedThisFrame | released;
        }

        return released;
    }
}

public interface IPlayerInput
{
    IWorldPosition WorldPointerPosition { get; }
    float MovementInput { get; }
    bool PrimaryActionKeyDown { get; }
    bool PrimaryActionKeyUp { get; }
    bool ObeliskKeyDown { get; }
    bool InvokeFireDown { get; }
    bool InvokeWaterDown { get; }
    bool InvokeEarthDown { get; }
    bool InvokeAirDown { get; }
    Vector2 PointerMovement { get; }
    Vector3 MouseRayHitPoint { get; }
    bool SecondaryActionKeyDown { get; }
    bool SecondaryActionKeyUp { get; }
    bool AttackActionKeyDown { get; }
    bool AttackActionKeyUp { get; }
    bool JumpLeftKeyDown { get; }
    bool JumpRightKeyDown { get; }
}