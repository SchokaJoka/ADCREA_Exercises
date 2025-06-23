using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    private SortedDictionary<float, string> spawnSchedule = new SortedDictionary<float, string>();
    private List<string> enemyTypes = new List<string> { "Goblin", "Orc", "Troll", "Skeleton", "Zombie" };

    void Start()
    {
        // 10 zufällige Spawns zwischen 2 und 30 Sekunden
        for (int i = 0; i < 10; i++)
        {
            float spawnTime = Random.Range(2f, 30f);
            string enemyType = enemyTypes[Random.Range(0, enemyTypes.Count)];
            // Sicherstellen, dass der Key eindeutig ist
            while (spawnSchedule.ContainsKey(spawnTime))
            {
                spawnTime += 0.01f;
            }
            spawnSchedule.Add(spawnTime, enemyType);
        }
    }

    void Update()
    {
        if (spawnSchedule.Count == 0) return;
        float nextSpawnTime = float.MaxValue;
        string nextEnemy = null;
        // Immer das erste Element holen (kleinster Key)
        foreach (var kvp in spawnSchedule)
        {
            nextSpawnTime = kvp.Key;
            nextEnemy = kvp.Value;
            break;
        }
        if (Time.time >= nextSpawnTime)
        {
            Debug.Log($"Spawn Enemy: {nextEnemy} at {nextSpawnTime:F2}s (GameTime: {Time.time:F2}s)");
            spawnSchedule.Remove(nextSpawnTime);
            // Hier könnte man stattdessen ein Prefab instanziieren
        }
    }
}
