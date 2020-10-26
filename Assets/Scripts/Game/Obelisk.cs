using UnityEngine;

public class Obelisk : MonoBehaviour
{
    public Transform EssenceHolder => essenceHolder;
    public Essence CurrentEssence { get; private set; }
    [SerializeField] private Transform essenceHolder;
    [SerializeField] private MeshRenderer mRenderer;
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material highlightMaterial;
    public void Highlight()
    {
        mRenderer.material = highlightMaterial;
    }

    public void Dehighlight()
    {
        mRenderer.material = normalMaterial;
    }

    public void AddEssence(Essence essence)
    {
        CurrentEssence = essence;
        essence.transform.position = EssenceHolder.position;
    }
}