using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;
    public int spawnCount = 1;
    public float spawnRadius = 5f;
    public bool isActive = true;

    private float timer;
    private UnitStats stats; // 이 스포너 건물의 체력 관리

    void Awake()
    {
        stats = GetComponent<UnitStats>();
        if (stats != null)
            stats.OnDeath += OnSpawnerDestroyed;
    }

    void Update()
    {
        if (!isActive) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPos.y = 0f;
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }

    void OnSpawnerDestroyed()
    {
        isActive = false; // 건물 부서지면 스폰 중단
    }
}