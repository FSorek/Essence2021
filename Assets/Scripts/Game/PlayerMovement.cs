using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float panSpeed = 1f;
    private Vector3 movement;

    public void Move(InputAction.CallbackContext context)
    {
        var movementValue = context.ReadValue<float>() * panSpeed;
        movement = new Vector3(movementValue, 0 ,0);
    }

    private void LateUpdate()
    {
        transform.Translate(movement * Time.deltaTime);
    }
}

