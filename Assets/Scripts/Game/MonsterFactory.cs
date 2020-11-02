using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class MonsterFactory : IEntityFactory<Monster>
{
    public static event Action<Monster> OnMonsterCreated;
    private readonly List<Monster> monstersAlive = new List<Monster>();
    public IEnumerable<Monster> GetAliveEntities()
    {
        return monstersAlive;
    }

    public void CreateMonster(Monster monsterPrefab, Vector3 spawnPosition, Transform transformParent)
    {
        var monster = Object.Instantiate(monsterPrefab, spawnPosition, Quaternion.identity, transformParent);
        monster.MultiplyMovementSpeed(Random.Range(.8f, 1.5f));
        OnMonsterCreated?.Invoke(monster);
        monstersAlive.Add(monster);
        monster.OnDeath += MonsterOnDeath;
    }

    private void MonsterOnDeath(Monster monster)
    {
        if(!monstersAlive.Contains(monster))
            return;
        monstersAlive.Remove(monster);
        monster.OnDeath -= MonsterOnDeath;
    }
}