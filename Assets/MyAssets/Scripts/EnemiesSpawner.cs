using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour
{
    [Header("Class references")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private EnemyData[] enemyDataArray;
    [SerializeField] private EnemyData currentEnemyData;

    [Header("Enemies start position")]
    [SerializeField] private float xPos;
    [SerializeField] private float maxUpPos;
    [SerializeField] private float minDownPos;
    [SerializeField] private Vector2 enemyStartPos;
    [SerializeField] private Transform midBossStart;
    [SerializeField] private Transform finalBossStart;

    [Header("Waves info")]
    [SerializeField] private int currentWave;
    [SerializeField] private int maxWaves;

    [Header(" ")]
    [SerializeField] private GameObject enemyPrefab;
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

    public void StartNewWave()
    {
        currentWave++;
        enemiesCreated = 0;
        
        if (currentWave < maxWaves)
        {
            var yPos = Random.Range(maxUpPos, minDownPos);
            enemyStartPos = new Vector2(xPos, yPos);
            currentEnemyData = enemyDataArray[currentWave - 1];
            StartCoroutine(EnemyWaveRoutine());
        }
    }

    private IEnumerator EnemyWaveRoutine()
    {
        maxEnemies = currentEnemyData.maxEnemies;
        enemyPrefab = currentEnemyData.enemyPrefab;

        while (enemiesCreated < maxEnemies)
        {
            Instantiate(enemyPrefab, enemyStartPos, quaternion.identity);
            enemiesCreated++;
            gameManager.OnEnemyCreated(1);
            enemiesCount = gameManager.GetEnemiesAmount();
            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(3);
    }
}