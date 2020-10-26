using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerStateMachine : MonoBehaviour
{
    public Type CurrentStateType => stateMachine.CurrentState.GetType();
    private StateMachine stateMachine;
    [SerializeField] private Obelisk obeliskPrefab;
    [SerializeField] private Essence essenceOfFire;
    private void Start()
    {
        stateMachine = new StateMachine();
        stateMachine.OnStateChanged += HandleStateChanged;
        var player = GetComponent<Player>();
        var mouseOverObelisk = new MouseOverSelector(LayerMask.GetMask("Obelisk"), player.WorldPointer);
        var idle = new Idle();
        var absorb = new Absorb(player,mouseOverObelisk);
        var exude = new Exude(player, mouseOverObelisk);
        var placingObelisk = new PlacingObelisk(obeliskPrefab,player);
        
        var invokeFireElement = new InvokeElement(mouseOverObelisk);
        var invokeWaterElement = new InvokeElement(mouseOverObelisk);
        var invokeEarthElement = new InvokeElement(mouseOverObelisk);
        var invokeAirElement = new InvokeElement(mouseOverObelisk);
        
        var buildingFireElement = new Building(essenceOfFire, player);

        stateMachine.AddAnyTransition(placingObelisk, () => PlayerInput.Instance.ObeliskKeyDown && player.CurrentEssence == null);
        stateMachine.AddAnyTransition(invokeFireElement, () => PlayerInput.Instance.InvokeFireDown && player.CurrentEssence == null);
        
        stateMachine.AddTransition(placingObelisk, idle, () => PlayerInput.Instance.ObeliskKeyDown || placingObelisk.Finished);
        stateMachine.AddTransition(invokeFireElement, idle, () => PlayerInput.Instance.InvokeFireDown);
        stateMachine.AddTransition(invokeFireElement, buildingFireElement, () => PlayerInput.Instance.PrimaryActionKeyDown && invokeFireElement.CanBuild);
        stateMachine.AddTransition(buildingFireElement, invokeFireElement, () => PlayerInput.Instance.PrimaryActionKeyUp || buildingFireElement.Finished);
        stateMachine.AddTransition(idle, absorb, () => PlayerInput.Instance.SecondaryActionKeyDown && player.CurrentEssence == null && absorb.CanAbsorb);
        stateMachine.AddTransition(absorb, idle, () => PlayerInput.Instance.SecondaryActionKeyUp || absorb.Finished);
        stateMachine.AddTransition(idle, exude, () => PlayerInput.Instance.PrimaryActionKeyDown && player.CurrentEssence != null && exude.CanExtract);
        stateMachine.AddTransition(exude, idle, () => PlayerInput.Instance.PrimaryActionKeyUp || exude.Finished);

        stateMachine.SetState(idle);
    }

    private void HandleStateChanged(IState obj)
    {
        Debug.Log(obj.GetType());
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