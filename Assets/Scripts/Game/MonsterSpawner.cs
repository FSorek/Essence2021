using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private Monster monsterPrefab;
    [SerializeField][Min(.5f)] private float spawningFrequency;
    [SerializeField] private Collider spawnArea;
    private MonsterFactory factory;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), spawningFrequency, spawningFrequency);
        factory = WorldSettings.MonsterFactory;
    }

    private void Spawn()
    {
        var spawnPointX = transform.position.x - Random.Range(0, spawnArea.bounds.size.x);
        var spawnPointZ = transform.position.z - spawnArea.bounds.size.z/2 + Random.Range(0, spawnArea.bounds.size.z);
        var spawnPosition = new Vector3(spawnPointX, transform.position.y, spawnPointZ);

        factory.CreateMonster(monsterPrefab, spawnPosition, spawnArea.transform.parent);
    }
}