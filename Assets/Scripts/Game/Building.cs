using UnityEngine;

public class Building : IState
{
    public bool Finished { get; private set; }
    private readonly Essence essence;
    private readonly LineRenderer visual;
    private readonly Player player;
    private readonly float buildTime = 2f;
    private float timer;
    private Obelisk cachedTarget;

    public Building(Essence essence, Player player)
    {
        this.essence = essence;
        this.visual = player.BuildLine;
        this.player = player;
    }
    public void Tick()
    {
        timer -= Time.deltaTime;
        visual.SetPosition(0, player.WorldPointer.transform.position);
        if(timer > 0 || Finished)
            return;
        var createdEssence = Object.Instantiate(essence);
        cachedTarget.AddEssence(createdEssence);
        Finished = true;
    }

    public void OnEnter()
    {
        timer = buildTime;
        cachedTarget = player.ObeliskFinder.Target;
        visual.enabled = true;
        visual.SetPosition(1, cachedTarget.EssenceHolder.position);
        Finished = false;
    }

    public void OnExit()
    {
        visual.enabled = false;
    }
}