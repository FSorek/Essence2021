using UnityEngine;

public class PlacingObelisk : IState
{
    private static int mouseLayer;
    private static int nameIndex = 0;
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
        mouseLayer = LayerMask.NameToLayer("MouseRay");
    }
    public void Tick()
    {
        if(currentInstance == null)
            return;

        var pointerPosition = player.WorldPointer.transform.position;
        var obeliskPosition = PlayerInput.Instance.MouseRayHitPoint;
        var obeliskRotation = Quaternion.LookRotation(tracker.ClosestCollider.transform.forward, currentInstance.up);
        currentInstance.position = obeliskPosition;
        currentInstance.rotation = obeliskRotation;
        Debug.DrawLine(pointerPosition, PlayerInput.Instance.MouseRayHitPoint);
        
        if (PlayerInput.Instance.PrimaryActionKeyDown)
        {
            nameIndex++;
            currentInstance.gameObject.name += nameIndex;
            currentInstance.SetParent(tracker.ClosestCollider.transform);
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