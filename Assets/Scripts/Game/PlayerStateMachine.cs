using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public Type CurrentStateType => stateMachine.CurrentState.GetType();
    private StateMachine stateMachine;

    private void Awake()
    {
        stateMachine = new StateMachine();
        var idle = new Idle();
        var placingObelisk = new PlacingObelisk();
        
        stateMachine.AddTransition(idle, placingObelisk, () => PlayerInput.Instance.ObeliskKeyDown);
        
        stateMachine.SetState(idle);
    }

    private void Update()
    {
        stateMachine.Tick();
    }
}

public class Idle : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }
}

public class PlacingObelisk : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }
}
public interface IState
{
    void Tick();
    void OnEnter();
    void OnExit();
}
public class StateMachine
{
    public event Action<IState> OnStateChanged = delegate {  };
    
    private List<StateTransition> stateTransitions = new List<StateTransition>();
    private List<StateTransition> anyStateTransition = new List<StateTransition>();

    private IState currentState;
    public IState CurrentState => currentState;

    public void Tick()
    {
        StateTransition transition = CheckForTransition();
        if (transition != null)
        {
            SetState(transition.To);
        }
        
        currentState.Tick();
    }

    private StateTransition CheckForTransition()
    {
        foreach (var transition in anyStateTransition)
        {
            if (transition.Condition())
                return transition;
        }
        
        foreach (var transition in stateTransitions)
        {
            if (transition.From == currentState && transition.Condition())
                return transition;
        }

        return null;
    }

    public void AddTransition(IState from, IState to, Func<bool> condition)
    {

        var transition = new StateTransition(from, to, condition);
        stateTransitions.Add(transition);
    }
    public void AddAnyTransition(IState to, Func<bool> condition)
    {
        var transition = new StateTransition(null, to, condition);
        anyStateTransition.Add(transition);
    }
    public void SetState(IState state)
    {
        if(currentState == state) return;
        
        currentState?.OnExit();
        currentState = state;
        currentState.OnEnter();

        OnStateChanged(currentState);
    }
    
}
public class StateTransition
{
    public readonly IState From;
    public readonly IState To;
    public readonly Func<bool> Condition;

    public StateTransition(IState from, IState to, Func<bool> condition)
    {
        From = from;
        To = to;
        Condition = condition;
    }
}