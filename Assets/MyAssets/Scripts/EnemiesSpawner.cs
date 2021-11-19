using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [Header("Class references")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private EnemyWaveData[] enemyDataArray;
    [SerializeField] private EnemyWaveData currentEnemyWaveData;

    [Header("Waves info")]
    [SerializeField] private int currentWave;
    [SerializeField] private int maxWaves;

    [Header(" ")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Vector2 enemyStartPos;
    
    [SerializeField] private int maxEnemies;
    [SerializeField] private int enemiesCreated; // for testing
    [SerializeField] private int enemiesCount; // for testing

    private static EnemiesSpawner instance;
    public static EnemiesSpawner Instance => instance;

    private void Start()
    {
        instance = this;
        if(instance == null) Debug.LogError("EnemySpawner instance is null");
        
        gameManager = GameManager.Instance;
        StartNewWave();
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }

    public void StartNewWave()
    {
        currentWave++;
        
        if (currentWave < maxWaves)
        {
            currentEnemyWaveData = enemyDataArray[currentWave];
            StartCoroutine(EnemyWaveRoutine());
        }
    }

    private IEnumerator EnemyWaveRoutine()
    {
        yield return new WaitForSeconds(3);

        enemiesCreated = 0;
        maxEnemies = currentEnemyWaveData.maxEnemies;
        enemyPrefab = currentEnemyWaveData.enemyPrefab;

        while (enemiesCreated < maxEnemies)
        {
            Instantiate(enemyPrefab, enemyStartPos, quaternion.identity);
            enemiesCreated++;
            gameManager.OnEnemyCreated(1);
            
            yield return new WaitForSeconds(1);
        }
    }
}