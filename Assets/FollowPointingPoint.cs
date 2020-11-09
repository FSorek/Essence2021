using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPointingPoint : MonoBehaviour
{
    private void Update()
    {
        transform.position = PlayerInput.Instance.MouseRayHitPoint;
    }
}
