using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float panSpeed = 1f;

    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var movement = panSpeed * Time.deltaTime * new Vector3(horizontal, 0, 0);
        
        transform.Translate(movement);
    }
}
