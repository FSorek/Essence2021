using UnityEngine;

public class PlacingObelisk : IState
{
    private static int mouseLayer;
    private static int nameIndex = 0;
    public bool Finished { get; private set; }
    private readonly Obelisk prefab;
    private readonly Player player;
    private Transform currentInstance;

    public PlacingObelisk(Obelisk prefab, Player player)
    {
        this.prefab = prefab;
        this.player = player;
        mouseLayer = LayerMask.NameToLayer("MouseRay");
    }
    public void Tick()
    {
        if(currentInstance == null || player.ColliderTracker == null || player.ColliderTracker.ClosestCollider == null)
            return;

        var pointerPosition = player.WorldPointer.transform.position;
        var obeliskPosition = PlayerInput.Instance.MouseRayHitPoint;
        var obeliskRotation = Quaternion.LookRotation(player.ColliderTracker.ClosestCollider.transform.forward, currentInstance.up);
        currentInstance.position = obeliskPosition;
        currentInstance.rotation = obeliskRotation;
        Debug.DrawLine(pointerPosition, PlayerInput.Instance.MouseRayHitPoint);
        
        if (PlayerInput.Instance.PrimaryActionKeyDown)
        {
            nameIndex++;
            currentInstance.gameObject.name += nameIndex;
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