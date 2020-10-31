using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory : IEntityFactory<Monster>
{
    private readonly List<Monster> monstersAlive = new List<Monster>();
    
    public IEnumerable<Monster> GetAliveEntities()
    {
        return monstersAlive;
    }

    public void CreateMonster(Monster monsterPrefab, Vector3 spawnPosition, Transform transformParent)
    {
        var monster = Object.Instantiate(monsterPrefab, spawnPosition, Quaternion.identity, transformParent);
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