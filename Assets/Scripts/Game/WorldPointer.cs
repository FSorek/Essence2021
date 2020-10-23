using System;
using UnityEngine;

[RequireComponent(typeof(WorldPosition))]
public class WorldPointer : MonoBehaviour
{
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private Renderer localRenderer;
    private Camera playerCamera;
    private WorldPosition position;
    private IPlayerInput playerInput;

    private void Awake()
    {
        position = GetComponent<WorldPosition>();
        playerCamera = Camera.main;
    }

    private void Start()
    {
        playerInput = PlayerInput.Instance;
        transform.localPosition = new Vector3(0,-10,0);
    }

    private void Update()
    {
        var pointerMovement = sensitivity * Time.deltaTime * playerInput.PointerMovement;
        transform.Translate(new Vector3(pointerMovement.x, 0, pointerMovement.y));
        ClampPosition();
    }

    private void ClampPosition()
    {
        Vector3 viewportPosition = playerCamera.WorldToViewportPoint (transform.position);
        viewportPosition.x = Mathf.Clamp(viewportPosition.x,.02f,.98f);
        viewportPosition.y = Mathf.Clamp(viewportPosition.y,.02f,.98f);
        var clampedPosition = playerCamera.ViewportToWorldPoint(viewportPosition);
        transform.position = new Vector3(clampedPosition.x, transform.position.y, clampedPosition.z);
    }
}