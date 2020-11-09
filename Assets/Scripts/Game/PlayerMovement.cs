using System;
using UnityEngine;

[RequireComponent(typeof(WorldPosition))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float panSpeed = 1f;
    private Vector3 movement;
    private IPlayerInput playerInput;
    private IWorldPosition position;

    private void Start()
    {
        playerInput = PlayerInput.Instance;
        position = GetComponent<IWorldPosition>();
    }

    private void Update()
    {
        if (playerInput.JumpLeftKeyDown)
        {
            Vector3 jumpVector = Vector3.zero;
            float percentagePosition = position.SegmentPosition / position.CurrentSegment.Length;

            if (percentagePosition < .4f)
            {
                var distanceToMiddle = position.CurrentSegment.Length / 2 - position.SegmentPosition;
                jumpVector = new Vector3(-distanceToMiddle, 0);
            }
            else if(percentagePosition >= .4f && percentagePosition < .9f)
            {
                var distanceToLeftEnd = position.CurrentSegment.Length - position.SegmentPosition - .1f;
                jumpVector = new Vector3(-distanceToLeftEnd, 0);
            }
            else
            {
                var distanceToLeftSegmentMiddle = position.LeftSegment.Length / 2 + position.CurrentSegment.Length - position.SegmentPosition;
                jumpVector = new Vector3(-distanceToLeftSegmentMiddle,0);
            }
            transform.position += jumpVector;
        }
        if (playerInput.JumpRightKeyDown)
        {
            Vector3 jumpVector = Vector3.zero;
            float percentagePosition = position.SegmentPosition / position.CurrentSegment.Length;

            if (percentagePosition <= .1f)
            {
                var distanceToRightSegmentMiddle = position.RightSegment.Length / 2 + position.SegmentPosition;
                jumpVector = new Vector3(distanceToRightSegmentMiddle,0);
            }
            else if (percentagePosition > .1f && percentagePosition < .6f)
            {
                var distanceToStart = position.SegmentPosition + .1f;
                jumpVector = new Vector3(distanceToStart, 0);
            }
            else
            {
                var distanceToMiddle = position.SegmentPosition - position.CurrentSegment.Length / 2;
                jumpVector = new Vector3(distanceToMiddle, 0);
            }
            transform.position += jumpVector;
        }
    }

    private void LateUpdate()
    {
        var movementVector = new Vector3(playerInput.MovementInput * Time.deltaTime * panSpeed, 0,0);
        transform.Translate(movementVector);
    }
}