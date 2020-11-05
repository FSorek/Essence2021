using UnityEngine;

public class Absorb : IState
{
    private static float absorbTime = 2f;
    public bool CanAbsorb => CheckCanAbsorb();
    public bool Finished { get; private set; }

    private readonly Player player;
    private readonly MouseOverSelector obeliskSelector;

    private Obelisk cachedTarget;
    private float timer;

    public Absorb(Player player, MouseOverSelector obeliskSelector)
    {
        this.player = player;
        this.obeliskSelector = obeliskSelector;
    }

    public void Tick()
    {
        timer -= Time.deltaTime;
        if(timer > 0)
            return;
        var extractedEssence = cachedTarget.ExtractEssence();
        player.AddEssence(extractedEssence);
        timer = absorbTime;
        Finished = true;
    }

    public void OnEnter()
    {
        Finished = false;
        timer = absorbTime;
    }

    public void OnExit()
    {
    }

    private bool CheckCanAbsorb()
    {
        var currentTarget = obeliskSelector.GetTarget(CanAbsorbOnTarget);
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

public class Exude : IState
{
    private static float extractTime = 2f;
    public bool CanExtract => CheckCanExtract();
    public bool Finished { get; private set; }

    private readonly Player player;
    private readonly MouseOverSelector obeliskSelector;

    private Obelisk cachedTarget;
    private float timer;

    public Exude(Player player, MouseOverSelector obeliskSelector)
    {
        this.player = player;
        this.obeliskSelector = obeliskSelector;
    }

    public void Tick()
    {
        timer -= Time.deltaTime;
        if(timer > 0)
            return;
        var extractedEssence = player.ExtractEssence();
        cachedTarget.AddEssence(extractedEssence);
        timer = extractTime;
        Finished = true;
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
        var currentTarget = obeliskSelector.GetTarget(CanExtractToTarget);
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