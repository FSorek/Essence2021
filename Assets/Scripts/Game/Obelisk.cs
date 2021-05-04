using System;
using UnityEngine;

public class Obelisk : MonoBehaviour, IEssenceHolder
{
    public Transform EssenceHolder => essenceHolder;
    public Essence CurrentEssence { get; private set; }
    [SerializeField] private Transform essenceHolder;
    [SerializeField] private GameObject incorrectCollisionIndicator;
    public bool HasCorrectCollision => isActivated || intersectingCollidersCount <= 0;
    private int intersectingCollidersCount;
    private bool isActivated;
    private SegmentCollider parentCollider;

    private void OnEnable()
    {
        isActivated = false;
        if(incorrectCollisionIndicator.activeSelf)
            incorrectCollisionIndicator.SetActive(false);
    }

    public void UpdatePosition(Vector3 position, Vector3 lootRotation)
    {
        transform.position = position;
        transform.rotation = Quaternion.LookRotation(lootRotation, transform.up);
    }
    public void Activate(SegmentCollider parentCollider)
    {
        isActivated = true;
        this.parentCollider = parentCollider;
        transform.SetParent(parentCollider.transform);
    }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Obelisk>() == null)
            return;
        intersectingCollidersCount++;
        if(!HasCorrectCollision && !incorrectCollisionIndicator.activeSelf)
            incorrectCollisionIndicator.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Obelisk>() == null)
            return;
        intersectingCollidersCount--;
        if(HasCorrectCollision && incorrectCollisionIndicator.activeSelf)
            incorrectCollisionIndicator.SetActive(false);
    }
}