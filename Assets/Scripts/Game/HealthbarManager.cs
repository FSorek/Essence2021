using System;
using UnityEngine;

public class HealthbarManager : MonoBehaviour
{
    [SerializeField] private UI_Health healthbarPrefab;
    [SerializeField] private RectTransform parent;

    private void Awake()
    {
        MonsterFactory.OnMonsterCreated += MonsterFactoryOnMonsterCreated;
    }
    private void MonsterFactoryOnMonsterCreated(Monster monster)
    {
        var healthbar = Instantiate(healthbarPrefab, parent);
        healthbar.SetTarget(monster);
    }
}