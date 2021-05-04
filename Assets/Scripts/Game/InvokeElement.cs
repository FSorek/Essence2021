using UnityEngine;

public class InvokeElement : IState
{
    public EssenceNames TargetElement { get; }
    public InvokeElement(EssenceNames targetElement)
    {
        TargetElement = targetElement;
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