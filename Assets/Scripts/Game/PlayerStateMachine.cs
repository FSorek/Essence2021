using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerStateMachine : MonoBehaviour
{
    public Type CurrentStateType => stateMachine.CurrentState.GetType();
    private StateMachine stateMachine;
    [SerializeField] private FireEssenceAttack fireAttack;
    [SerializeField] private Obelisk obeliskPrefab;
    private void Start()
    {
        stateMachine = new StateMachine();
        stateMachine.OnStateChanged += HandleStateChanged;
        var player = GetComponent<Player>();
        var mouseOverObelisk = new MouseOverSelector(LayerMask.GetMask("Obelisk"), 1f, 1);
        var idle = new Idle();
        var absorb = new Absorb(player, mouseOverObelisk);
        var exude = new Exude(player, mouseOverObelisk);
        
        var placingObelisk = new PlacingObelisk(obeliskPrefab, player);
        
        var buildingFireElement = new Building(mouseOverObelisk, EssenceNames.Fire);
        
        var invokeFireElement = new InvokeElement(mouseOverObelisk);
        var invokeWaterElement = new InvokeElement(mouseOverObelisk);
        var invokeEarthElement = new InvokeElement(mouseOverObelisk);
        var invokeAirElement = new InvokeElement(mouseOverObelisk);
        
       
        var attack = new Attack(fireAttack);

        stateMachine.AddAnyTransition(placingObelisk, () => PlayerInput.Instance.ObeliskKeyDown);
        stateMachine.AddAnyTransition(invokeFireElement, () => PlayerInput.Instance.InvokeFireDown && player.CurrentEssence == null);
        
        stateMachine.AddTransition(placingObelisk, idle, () => PlayerInput.Instance.ObeliskKeyDown || placingObelisk.Finished);
        stateMachine.AddTransition(invokeFireElement, idle, () => PlayerInput.Instance.InvokeFireDown);
        stateMachine.AddTransition(invokeFireElement, buildingFireElement, () => PlayerInput.Instance.PrimaryActionKeyDown && invokeFireElement.CanBuild);
        stateMachine.AddTransition(buildingFireElement, invokeFireElement, () => PlayerInput.Instance.PrimaryActionKeyUp || buildingFireElement.Finished);
        
        stateMachine.AddTransition(idle, absorb, () => PlayerInput.Instance.SecondaryActionKeyDown && player.CurrentEssence == null && absorb.CanAbsorb);
        stateMachine.AddTransition(absorb, idle, () => PlayerInput.Instance.SecondaryActionKeyUp || absorb.Finished);
        
        stateMachine.AddTransition(idle, exude, () => PlayerInput.Instance.PrimaryActionKeyDown && player.CurrentEssence != null && exude.CanExtract);
        stateMachine.AddTransition(exude, idle, () => PlayerInput.Instance.PrimaryActionKeyUp || exude.Finished);
        
        stateMachine.AddTransition(idle, attack, () => PlayerInput.Instance.AttackActionKeyDown && player.CurrentEssence != null);
        stateMachine.AddTransition(attack, idle, () => PlayerInput.Instance.AttackActionKeyUp);

        stateMachine.SetState(idle);
        
        player.Visuals.AssignBuildingState(buildingFireElement);
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

public class BuildingData : ScriptableObject
{
    public MouseOverSelector ObeliskSelector => obeliskSelector;
    public Essence EssencePrefab => essencePrefab;
    
    private const float selectorRadius = 1f;
    private const int targetCap = 1;
    [SerializeField] private Essence essencePrefab;
    [SerializeField] private EssenceNames essenceName;
    private MouseOverSelector obeliskSelector;
    private void Awake()
    {
        if (obeliskSelector != null) 
            return;
        
        var obeliskLayer = LayerMask.GetMask("Obelisk");
        obeliskSelector = new MouseOverSelector(obeliskLayer, selectorRadius, targetCap);
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
public enum EssenceNames
{
    Fire,
    Water,
    Earth,
    Air,
}