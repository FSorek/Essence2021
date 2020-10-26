using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerStateMachine : MonoBehaviour
{
    public Type CurrentStateType => stateMachine.CurrentState.GetType();
    [SerializeField] private SegmentColliderTracker tracker;
    [SerializeField] private Obelisk obeliskPrefab;
    [SerializeField] private Essence essenceOfFire;
    private StateMachine stateMachine;

    private void Start()
    {
        stateMachine = new StateMachine();
        var player = GetComponent<Player>();
        var idle = new Idle();
        var placingObelisk = new PlacingObelisk(obeliskPrefab,tracker,player);
        var mouseOverEmptyObelisk = new MouseOverSelector(LayerMask.GetMask("Obelisk"), player.WorldPointer);
        
        var invokeFireElement = new InvokeElement(mouseOverEmptyObelisk);
        var invokeWaterElement = new InvokeElement(mouseOverEmptyObelisk);
        var invokeEarthElement = new InvokeElement(mouseOverEmptyObelisk);
        var invokeAirElement = new InvokeElement(mouseOverEmptyObelisk);
        
        var buildingFireElement = new Building(essenceOfFire, player);

        stateMachine.AddAnyTransition(placingObelisk, () => PlayerInput.Instance.ObeliskKeyDown);
        stateMachine.AddAnyTransition(invokeFireElement, () => PlayerInput.Instance.InvokeFireDown);
        
        stateMachine.AddTransition(placingObelisk, idle, () => PlayerInput.Instance.ObeliskKeyDown || placingObelisk.Finished);
        stateMachine.AddTransition(invokeFireElement, idle, () => PlayerInput.Instance.InvokeFireDown);
        stateMachine.AddTransition(invokeFireElement, buildingFireElement, () => PlayerInput.Instance.PrimaryActionKeyDown && invokeFireElement.CanBuild);
        stateMachine.AddTransition(buildingFireElement, invokeFireElement, () => PlayerInput.Instance.PrimaryActionKeyUp || buildingFireElement.Finished);

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

public interface IState
{
    void Tick();
    void OnEnter();
    void OnExit();
}