using UnityEngine;
using UnityEngine.VFX;

public class InvokeElementVFX : MonoBehaviour
{
    public EssenceNames TargetName { get; }
    [SerializeField] private EssenceNames targetName;
    [SerializeField] private VisualEffect[] visualEffects;

    public void Play()
    {
        foreach (var effect in visualEffects)
        {
            effect.Play();
        }
    }

    public void Stop()
    {
        foreach (var effect in visualEffects)
        {
            effect.Stop();
        }
    }
}