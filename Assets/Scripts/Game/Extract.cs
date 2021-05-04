using UnityEngine;

public class Extract : IState
{
    private static float extractTime = 2f;
    public bool CanExtract => CheckCanExtract();
    public bool Finished { get; private set; }
    public Vector3 TargetPosition => cachedTarget.EssenceHolder.position;

    private readonly Player player;
    private readonly MouseOverSelector infusedObeliskSelector;

    private Obelisk cachedTarget;
    private float timer;

    public Extract(Player player, MouseOverSelector infusedObeliskSelector)
    {
        this.player = player;
        this.infusedObeliskSelector = infusedObeliskSelector;
    }

    public void Tick()
    {
        timer -= Time.deltaTime;
        if(timer > 0 || Finished)
            return;
        Finished = true;
        var extractedEssence = cachedTarget.ExtractEssence();
        player.AddEssence(extractedEssence);
        timer = extractTime;
    }

    public void OnEnter()
    {
        Finished = false;
        timer = extractTime;
    }

    public void OnExit()
    {
    }

    private bool CheckCanExtract()
    {
        var currentTarget = infusedObeliskSelector.GetTarget();
        if (currentTarget == null)
            return false;
        cachedTarget = currentTarget.GetComponent<Obelisk>();
        return cachedTarget != null;
    }

    private bool CanAbsorbOnTarget(Collider collider)
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
}

public class Infuse : IState
{
    private static float infusionTime = 2f;
    public bool CanInfuse => CheckCanInfuse();
    public bool Finished { get; private set; }
    public Vector3 TargetPosition => cachedTarget.EssenceHolder.position;

    private readonly Player player;
    private readonly MouseOverSelector emptyObeliskSelector;

    private Obelisk cachedTarget;
    private float timer;

    public Infuse(Player player, MouseOverSelector emptyObeliskSelector)
    {
        this.player = player;
        this.emptyObeliskSelector = emptyObeliskSelector;
    }

    public void Tick()
    {
        timer -= Time.deltaTime;
        if(timer > 0)
            return;
        var extractedEssence = player.ExtractEssence();
        cachedTarget.AddEssence(extractedEssence);
        timer = infusionTime;
        Finished = true;
    }

    public void OnEnter()
    {
        Finished = false;
        timer = infusionTime;
    }

    public void OnExit()
    {
    }

    private bool CheckCanInfuse()
    {
        var currentTarget = emptyObeliskSelector.GetTarget();
        if (currentTarget == null)
            return false;
        cachedTarget = currentTarget.GetComponent<Obelisk>();
        return cachedTarget != null;
    }

    private bool CanExtractToTarget(Collider collider)
    {
        if (collider == null)
            return false;
        var holder = collider.GetComponent<IEssenceHolder>();
        if (holder == null)
            return false;

        if (holder.CurrentEssence != null)
            return false;
        
        return true;
    }
}

public interface IEssenceHolder
{
    Essence CurrentEssence { get; }
    void AddEssence(Essence essence);
    Essence ExtractEssence();
}