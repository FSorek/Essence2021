using UnityEngine;

public class PlacingObelisk : IState
{
    public bool Finished { get; private set; }
    private readonly Obelisk prefab;
    private readonly Player player;
    private Transform currentInstance;

    public PlacingObelisk(Obelisk prefab, Player player)
    {
        this.prefab = prefab;
        this.player = player;
    }
    public void Tick()
    {
        if(currentInstance == null 
           || player.ColliderTracker == null 
           || player.ColliderTracker.ClosestCollider == null)
            return;

        var obeliskPosition = PlayerInput.Instance.MouseRayHitPoint;
        var obeliskRotation = Quaternion.LookRotation(player.ColliderTracker.ClosestCollider.transform.forward, currentInstance.up);
        currentInstance.position = obeliskPosition;
        currentInstance.rotation = obeliskRotation;
        
        if (PlayerInput.Instance.PrimaryActionKeyDown)
        {
            currentInstance.SetParent(player.ColliderTracker.ClosestCollider.transform);
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