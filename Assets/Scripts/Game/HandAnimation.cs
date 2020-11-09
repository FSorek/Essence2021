using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerStateMachine stateMachine;
    private static readonly int Invoking = Animator.StringToHash("IsInvoking");
    private static readonly int IsBuilding = Animator.StringToHash("IsBuilding");
    private static readonly int Extracting = Animator.StringToHash("IsExtracting");

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        stateMachine = GetComponent<PlayerStateMachine>();
        stateMachine.OnStateEntered += HandleStateChanged;
        stateMachine.OnStateExited += HandleStateChanged;
    }

    private void HandleStateChanged(IState state)
    {
        if (state is InvokeElement)
        {
            FlipAnimatorBool(Invoking);
        }
        if (state is Building)
        {
            FlipAnimatorBool(IsBuilding);
        }
        if (state is Extract)
        {
            FlipAnimatorBool(Extracting);
        }
    }

    private void FlipAnimatorBool(int id)
    {
        var currentValue = animator.GetBool(id);
        animator.SetBool(id, !currentValue);
    }
}
