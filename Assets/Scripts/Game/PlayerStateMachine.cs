using System;
using UnityEngine;
using Object = UnityEngine.Object;

[RequireComponent(typeof(Player))]
public class PlayerStateMachine : MonoBehaviour
{
    public Type CurrentStateType => stateMachine.CurrentState.GetType();
    [SerializeField] private SegmentColliderTracker tracker;
    [SerializeField] private Obelisk obeliskPrefab;
    [SerializeField] private GameObject fireElementPrefab;
    private StateMachine stateMachine;

    private void Start()
    {
        stateMachine = new StateMachine();
        var player = GetComponent<Player>();
        var idle = new Idle();
        var placingObelisk = new PlacingObelisk(obeliskPrefab,tracker,player);
        
        var invokeFireElement = new InvokeElement(player);
        var invokeWaterElement = new InvokeElement(player);
        var invokeEarthElement = new InvokeElement(player);
        var invokeAirElement = new InvokeElement(player);
        
        var buildingFireElement = new Building(fireElementPrefab);

        stateMachine.AddTransition(idle, placingObelisk, () => PlayerInput.Instance.ObeliskKeyDown);
        stateMachine.AddTransition(placingObelisk, idle, () => PlayerInput.Instance.ObeliskKeyDown || placingObelisk.Finished);
        stateMachine.AddTransition(idle, invokeFireElement, () => PlayerInput.Instance.InvokeFireDown);
        stateMachine.AddTransition(invokeFireElement, buildingFireElement, () => PlayerInput.Instance.PrimaryActionKeyDown && invokeFireElement.CanBuild);
        stateMachine.AddTransition(buildingFireElement, invokeFireElement, () => PlayerInput.Instance.PrimaryActionKeyUp);

        stateMachine.SetState(idle);
    }

    private void Update()
    {
        stateMachine.Tick();
        Debug.Log(CurrentStateType);
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
    public bool Finished { get; private set; }
    private readonly Obelisk prefab;
    private readonly SegmentColliderTracker tracker;
    private readonly Player player;
    private Transform currentInstance;

    public PlacingObelisk(Obelisk prefab, SegmentColliderTracker tracker, Player player)
    {
        this.prefab = prefab;
        this.tracker = tracker;
        this.player = player;
    }
    public void Tick()
    {
        if(currentInstance == null)
            return;
        currentInstance.position = tracker.ClosestCollider.ClosestPoint(player.WorldPointer.transform.position);
        currentInstance.rotation = Quaternion.LookRotation(tracker.ClosestCollider.transform.up, currentInstance.up);
        if (PlayerInput.Instance.PrimaryActionKeyDown)
        {
            currentInstance = null;
            Finished = true;
        }
    }

    public void OnEnter()
    {
        currentInstance = Object.Instantiate(prefab).transform;
        Finished = false;
    }

    public void OnExit()
    {
        if(currentInstance == null)
            return;
        
        Object.Destroy(currentInstance.gameObject);
        currentInstance = null;
    }
}
 
public class InvokeElement : IState
{
    public bool CanBuild => nearbyObelisks.Length > 0;
    private readonly float buildRadius = 20f;
    private readonly float updateFrequency = .5f;
    private readonly Player player;
    private readonly LayerMask obeliskLayer = LayerMask.NameToLayer("Obelisk");
    private Collider[] nearbyObelisks;
    private float updateTimer;

    public InvokeElement(Player player)
    {
        this.player = player;
    }
    public void Tick()
    {
        updateTimer -= Time.deltaTime;
        if(updateTimer > 0)
            return;

        nearbyObelisks = GetNearbyObelisks();
        updateTimer = updateFrequency;
    }

    private Collider[] GetNearbyObelisks()
    {
        return Physics.OverlapSphere(player.WorldPointer.transform.position, buildRadius, obeliskLayer);
    }

    public void OnEnter()
    {
        updateTimer = updateFrequency;
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