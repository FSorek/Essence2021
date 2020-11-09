using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

public class Building : IState
{
    public bool Finished { get; private set; }
    public EssenceNames Essence => essence;
    public Vector3 TargetPosition => cachedTarget.EssenceHolder.position;
    private readonly MouseOverSelector obeliskSelector;
    private readonly EssenceNames essence;
    private readonly float buildTime = 2f;
    private float timer;
    private Obelisk cachedTarget;

    public Building(MouseOverSelector obeliskSelector, EssenceNames essence)
    {
        this.obeliskSelector = obeliskSelector;
        this.essence = essence;
    }
    public void Tick()
    {
        timer -= Time.deltaTime;
        if(timer > 0 || Finished)
            return;
        var spawnPosition = PlayerInput.Instance.MouseRayHitPoint;
        var createdEssence = WorldSettings.EssenceFactory.CreateEssence(essence, spawnPosition);
        Finished = true;
        cachedTarget.AddEssence(createdEssence);
    }

    public void OnEnter()
    {
        var selectedTarget = obeliskSelector.GetTarget().GetComponent<Obelisk>();
        timer = buildTime;
        cachedTarget = selectedTarget;
        Finished = false;
    }

    public void OnExit()
    {
    }
}

public class EssenceFactory
{
    private readonly Transform parent;
    private Dictionary<EssenceNames, Essence> essences;

    public EssenceFactory(EssenceSet[] essenceSets, Transform parent)
    {
        this.parent = parent;
        essences = new Dictionary<EssenceNames, Essence>();
        foreach (var set in essenceSets)
        {
            if(essences.ContainsKey(set.Name))
                continue;
            
            essences.Add(set.Name, set.Essence);
        }
    }

    public Essence CreateEssence(EssenceNames name, Vector3 position)
    {
        var essence = Add(name);

        essence.transform.position = position;
        essence.transform.SetParent(parent);
        return essence;
    }

    private Essence Add(EssenceNames name)
    {
        if (!essences.ContainsKey(name)) name = EssenceNames.Null;
        return Object.Instantiate(essences[name]);
    }
}

[Serializable]
public class EssenceSet
{
    public EssenceNames Name;
    public Essence Essence;
}