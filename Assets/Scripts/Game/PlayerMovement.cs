using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float panSpeed = 1f;
    private Vector3 movement;
    private IPlayerInput playerInput;

    private void Start()
    {
        playerInput = PlayerInput.Instance;
    }

    private void LateUpdate()
    {
        var movementVector = new Vector3(playerInput.MovementInput * Time.deltaTime * panSpeed, 0,0);
        transform.Translate(movementVector);
    }
}