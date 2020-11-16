using UnityEngine;

public class Obelisk : MonoBehaviour, IEssenceHolder
{
    public Transform EssenceHolder => essenceHolder;
    public Essence CurrentEssence { get; private set; }
    [SerializeField] private Transform essenceHolder;

    public void AddEssence(Essence essence)
    {
        CurrentEssence = essence;
        essence.transform.position = EssenceHolder.position;
        CurrentEssence.gameObject.SetActive(true);
        CurrentEssence.transform.SetParent(EssenceHolder);
    }

    public Essence ExtractEssence()
    {
        var extractedEssence = CurrentEssence;
        CurrentEssence.gameObject.SetActive(false);
        CurrentEssence = null;
        return extractedEssence;
    }
}