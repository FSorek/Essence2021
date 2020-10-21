using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlayerStateMachine : MonoBehaviour
{
    public Type CurrentStateType => stateMachine.CurrentState.GetType();
    [SerializeField] private GameObject fireElementPrefab;
    private StateMachine stateMachine;

    private void Awake()
    {
        stateMachine = new StateMachine();
        var idle = new Idle();
        var placingObelisk = new PlacingObelisk();
        
        var invokeFireElement = new InvokeElement();
        var invokeWaterElement = new InvokeElement();
        var invokeEarthElement = new InvokeElement();
        var invokeAirElement = new InvokeElement();
        
        var buildingFireElement = new Building(fireElementPrefab);

        stateMachine.AddTransition(idle, placingObelisk, () => PlayerInput.Instance.ObeliskKeyDown);
        stateMachine.AddTransition(idle, invokeFireElement, () => PlayerInput.Instance.InvokeFireDown);
        stateMachine.AddTransition(invokeFireElement, buildingFireElement, () => PlayerInput.Instance.PrimaryActionKeyDown && invokeFireElement.CanBuild);
        stateMachine.AddTransition(buildingFireElement, invokeFireElement, () => PlayerInput.Instance.PrimaryActionKeyUp);

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
public class Building : IState
{
    private readonly GameObject buildPrefab;

    public Building(GameObject buildPrefab)
    {
        this.buildPrefab = buildPrefab;
    }
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
public class InvokeElement : IState
{
    public bool CanBuild { get; private set; } = true; // temporary
    public void Tick()
    {
    }

    public void OnEnter()
    {
        //obelisks = Object.FindObjectsOfType<Obelisk>();
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