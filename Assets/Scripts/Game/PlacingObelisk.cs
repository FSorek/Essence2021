using UnityEngine;

public class PlacingObelisk : IState
{
    public bool Finished { get; private set; }
    private readonly Obelisk prefab;
    private readonly Player player;
    private Obelisk currentInstance;

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
        var obeliskRotation = player.ColliderTracker.ClosestCollider.transform.forward;
        currentInstance.UpdatePosition(obeliskPosition, obeliskRotation);
        
        if (PlayerInput.Instance.PrimaryActionKeyDown && currentInstance.HasCorrectCollision)
        {
            currentInstance.Activate(player.ColliderTracker.ClosestCollider);
            currentInstance = null;
            Finished = true;
        }
    }
    
    public void OnEnter()
    {
        currentInstance = Object.Instantiate(prefab, new Vector3(0,-100,0), Quaternion.identity);
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