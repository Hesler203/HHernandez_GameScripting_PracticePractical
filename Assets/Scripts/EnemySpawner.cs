using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnCount;
    private Transform playerPosition;
    private int enemyCount;

    void Start()
    {
        playerPosition = FindAnyObjectByType<PlayerController>().transform;
        SpawnInitialEnemies();
    }

    void Update()
    {
        if (enemyCount == 0)
        {
            InvokeRepeating("SpawnRecurringEnemies", spawnInterval, spawnCount);
        }
    }

    private void SpawnInitialEnemies()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            SpawnEnemies(spawnPoint);
        }
    }

    private void SpawnRecurringEnemies()
    {
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        SpawnEnemies(spawnPoints[randomSpawnIndex]);
    }
    private void SpawnEnemies(Transform spawnPoint)
    {
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject spawnedEnemy = Instantiate(enemyPrefabs[randomEnemyIndex], spawnPoint);
        spawnedEnemy.name = enemyPrefabs[randomEnemyIndex].name;
        Character.Type spawnedEnemyType = Character.Type.none;
        switch (spawnedEnemy.name)
        {
            case nameof(Character.Type.enemySphere):
                spawnedEnemyType = Character.Type.enemySphere;
                break;
            case nameof(Character.Type.enemyCube):
                spawnedEnemyType = Character.Type.enemyCube;
                break;
            case nameof(Character.Type.enemyCylinder):
                spawnedEnemyType = Character.Type.enemyCylinder;
                break;
        }
        spawnedEnemy.GetComponent<EnemyController>().InitializeEnemy(spawnedEnemyType);
        spawnedEnemy.GetComponent<EnemyMovement>().InitializeTarget(playerPosition);
        enemyCount++;
    }

    public void DecreaseEnemyCount()
    {
        enemyCount--;
    }
}
