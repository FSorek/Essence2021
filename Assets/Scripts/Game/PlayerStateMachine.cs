using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerStateMachine : MonoBehaviour
{
    public Type CurrentStateType => stateMachine.CurrentState.GetType();
    public event Action<IState> OnStateEntered;
    public event Action<IState> OnStateExited;
    private StateMachine stateMachine;
    [SerializeField] private FireEssenceAttack fireAttack;
    [SerializeField] private Obelisk obeliskPrefab;
    private void Start()
    {
        stateMachine = new StateMachine();
        stateMachine.OnStateEntered += StateMachineOnOnStateEntered;
        stateMachine.OnStateExited += StateMachineOnStateExited;
        var player = GetComponent<Player>();
        var obeliskSelector = new PhysicsLayerStrategy(LayerMask.GetMask("Obelisk"), 1f);
        var emptyObeliskMouseSelector = new MouseOverSelector(obeliskSelector, 1, IsObeliskEmpty);
        var infusedObeliskMouseSelector = new MouseOverSelector(obeliskSelector, 1, IsObeliskFull);
        var idle = new Idle();
        var absorb = new Extract(player, infusedObeliskMouseSelector);
        var exude = new Infuse(player, emptyObeliskMouseSelector);
        
        var placingObelisk = new PlacingObelisk(obeliskPrefab, player);
        
        var invokeFireElement = new InvokeElement(EssenceNames.Fire);
        var invokeWaterElement = new InvokeElement(EssenceNames.Water);
        var invokeEarthElement = new InvokeElement(EssenceNames.Earth);
        var invokeAirElement = new InvokeElement(EssenceNames.Air);

        var buildingFireElement = new Building(emptyObeliskMouseSelector, EssenceNames.Fire);
        var buildingWaterElement = new Building(emptyObeliskMouseSelector, EssenceNames.Water);
        var buildingEarthElement = new Building(emptyObeliskMouseSelector, EssenceNames.Earth);
        var buildingAirElement = new Building(emptyObeliskMouseSelector, EssenceNames.Air);
       
        var attack = new Attack(fireAttack);
        
        stateMachine.AddAnyTransition(placingObelisk, () => PlayerInput.Instance.ObeliskKeyDown);
        stateMachine.AddTransition(placingObelisk, idle, () => PlayerInput.Instance.ObeliskKeyDown || placingObelisk.Finished || PlayerInput.Instance.SecondaryActionKeyDown);
        
        stateMachine.AddAnyTransition(absorb, () => PlayerInput.Instance.SecondaryActionKeyDown && player.CurrentEssence == null && absorb.CanExtract);
        stateMachine.AddTransition(absorb, idle, () => PlayerInput.Instance.SecondaryActionKeyUp || absorb.Finished);
        
        stateMachine.AddAnyTransition(invokeFireElement, () => PlayerInput.Instance.InvokeFireDown && player.CurrentEssence == null);
        stateMachine.AddAnyTransition(invokeWaterElement, () => PlayerInput.Instance.InvokeWaterDown && player.CurrentEssence == null);
        stateMachine.AddAnyTransition(invokeEarthElement, () => PlayerInput.Instance.InvokeEarthDown && player.CurrentEssence == null);
        stateMachine.AddAnyTransition(invokeAirElement, () => PlayerInput.Instance.InvokeAirDown && player.CurrentEssence == null);
        
        stateMachine.AddTransition(invokeFireElement, idle, () => PlayerInput.Instance.InvokeFireDown || PlayerInput.Instance.SecondaryActionKeyDown);
        stateMachine.AddTransition(invokeWaterElement, idle, () => PlayerInput.Instance.InvokeWaterDown || PlayerInput.Instance.SecondaryActionKeyDown);
        stateMachine.AddTransition(invokeEarthElement, idle, () => PlayerInput.Instance.InvokeEarthDown || PlayerInput.Instance.SecondaryActionKeyDown);
        stateMachine.AddTransition(invokeAirElement, idle, () => PlayerInput.Instance.InvokeAirDown || PlayerInput.Instance.SecondaryActionKeyDown);
        
        stateMachine.AddTransition(invokeFireElement, buildingFireElement, () => CanStartBuilding(emptyObeliskMouseSelector));
        stateMachine.AddTransition(invokeWaterElement, buildingWaterElement, () => CanStartBuilding(emptyObeliskMouseSelector));
        stateMachine.AddTransition(invokeEarthElement, buildingEarthElement, () => CanStartBuilding(emptyObeliskMouseSelector));
        stateMachine.AddTransition(invokeAirElement, buildingAirElement, () => CanStartBuilding(emptyObeliskMouseSelector));
        
        stateMachine.AddTransition(buildingFireElement, invokeFireElement, () => PlayerInput.Instance.PrimaryActionKeyUp || buildingFireElement.Finished);
        stateMachine.AddTransition(buildingWaterElement, invokeWaterElement, () => PlayerInput.Instance.PrimaryActionKeyUp || buildingWaterElement.Finished);
        stateMachine.AddTransition(buildingEarthElement, invokeEarthElement, () => PlayerInput.Instance.PrimaryActionKeyUp || buildingEarthElement.Finished);
        stateMachine.AddTransition(buildingAirElement, invokeAirElement, () => PlayerInput.Instance.PrimaryActionKeyUp || buildingAirElement.Finished);
        
       
        stateMachine.AddTransition(idle, exude, () => PlayerInput.Instance.PrimaryActionKeyDown && player.CurrentEssence != null && exude.CanInfuse);
        stateMachine.AddTransition(exude, idle, () => PlayerInput.Instance.PrimaryActionKeyUp || exude.Finished);
        
        stateMachine.AddTransition(idle, attack, () => PlayerInput.Instance.AttackActionKeyDown && player.CurrentEssence != null);
        stateMachine.AddTransition(attack, idle, () => PlayerInput.Instance.AttackActionKeyUp);

        stateMachine.SetState(idle);
    }

    private void StateMachineOnOnStateEntered(IState obj)
    {
        OnStateEntered?.Invoke(obj);
    }

    private void StateMachineOnStateExited(IState obj)
    {
        OnStateExited?.Invoke(obj);
    }

    private bool CanStartBuilding(MouseOverSelector mouseOverObelisk)
    {
        return PlayerInput.Instance.PrimaryActionKeyDown && mouseOverObelisk.GetTarget() != null;
    }
    private bool IsObeliskEmpty(Collider collider)
    {
        if (collider == null)
            return false;
        var obelisk = collider.GetComponent<Obelisk>();
        if (obelisk == null)
            return false;

        if (obelisk.CurrentEssence != null)
            return false;
        
        return true;
    }
    private bool IsObeliskFull(Collider collider)
    {
        if (collider == null)
            return false;
        var obelisk = collider.GetComponent<IEssenceHolder>();
        if (obelisk == null)
            return false;

        if (obelisk.CurrentEssence == null)
            return false;
        
        return true;
    }
    private void Update()
    {
        stateMachine.Tick();
    }
}