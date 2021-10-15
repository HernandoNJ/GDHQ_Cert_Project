using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private float xPos;
    [SerializeField] private float maxUpPos;
    [SerializeField] private float minDownPos;
    [SerializeField] private int wavesAmount;
    [SerializeField] private int currentWave;
    [SerializeField] private int enemiesPerBasicWave;
    [SerializeField] private bool midBossDestroyed;
    [SerializeField] private GameObject enemiesParent;
    [SerializeField] private GameObject enemyPrefab;
    
    [Header("ENEMIES WAVE")]
    [Tooltip("The ammount of basic enemies per wave")]
    [SerializeField] private GameObject[] enemiesArray;
    
    [SerializeField] private GameObject midBossPrefab;
    [SerializeField] private GameObject finalBossPrefab;
    [SerializeField] private Transform midBossPos;
    [SerializeField] private Transform finalBossStartPos;

    private readonly WaitForSeconds timeToNextSpawn = new WaitForSeconds(0.25f);
    private readonly WaitForSeconds timeToNextWave = new WaitForSeconds(2);
    
    private static EnemiesSpawner instance;
    public static EnemiesSpawner Instance => instance;

    private void Awake() => instance = this;

    private void Start()
    {
        FillEnemiesArray();
        CheckIfInstanceIsNull();
        currentWave = 1;
        StartNewWave();
    }

    private static void CheckIfInstanceIsNull()
    {
        if (instance == null) Debug.LogError("Enemies spawner is null");
    }

    public int GetCurrentWave() => currentWave;

    private void StartNewWave()
    {
        if (currentWave <= 0 || currentWave > wavesAmount)
        {
            Debug.LogError("phase value not valid");
            throw new ArgumentOutOfRangeException(nameof(currentWave));
        }

        switch (currentWave)
        {
            case 1: case 2: case 3: case 4: case 5: case 6: case 7: case 9: case 10: case 11:
                StartCoroutine(SpawnEnemiesRoutine());
                break;
            case 8:
                InstantiateMidBoss();
                break;
            case 12:
                InstantiateFinalBoss();
                break;
        }

        currentWave++;
    }

    private void FillEnemiesArray()
    {
        var spawnPos = new Vector2(xPos, SetRandomYPosition());
        
        for (int i = 0; i < enemiesArray.Length; i++)
        {
            var newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            enemiesArray[i] = newEnemy;
            newEnemy.SetActive(false);
            newEnemy.transform.SetParent(enemiesParent.transform);
        }
    }

    private float SetRandomYPosition() => Random.Range(minDownPos, maxUpPos);

    private void InstantiateMidBoss()
    {
        Instantiate(midBossPrefab, midBossPos.position, Quaternion.identity);
    }

    private void InstantiateFinalBoss()
    {
        Instantiate(finalBossPrefab, finalBossStartPos.position, Quaternion.identity);
    }
    
    public void MidBossDestroyed() => StartNewWave();

    public static void FinalBossDestroyed()
    {
        GameManager.Instance.FinalBossDestroyed();
    }

    // Waves 1 to 7 and 9 to 11
    private IEnumerator SpawnEnemiesRoutine()
    {
        var enemiesAmount = 0;

        while (enemiesAmount < enemiesPerBasicWave)
        {
            enemiesArray[enemiesAmount].SetActive(true);
            enemiesAmount++;
            yield return timeToNextSpawn;
        }

        StartNewWave();
    }

    // TODO Ask Thom - Remove after asking
    private IEnumerator WavesRoutine()
    {
        yield return timeToNextWave;

        for (currentWave = 1; currentWave < enemiesPerBasicWave; currentWave++)
        {
            StartCoroutine(SpawnEnemiesRoutine());
            yield return timeToNextWave;
        }

        yield return timeToNextWave;

        // TODO Does this require yield return?
        if (currentWave == 5) InstantiateMidBoss();

        // TODO Ask Thom how to implement the next code

        if (midBossDestroyed)
        {
            yield return timeToNextWave;

            for (currentWave = 6; currentWave < 10; currentWave++)
            {
                StartCoroutine(SpawnEnemiesRoutine());
                yield return timeToNextWave;
            }
        }

        yield return timeToNextWave;

        if (currentWave == 10) InstantiateFinalBoss();

        // TODO how to get out from the routine?
        //if (finalBossDestroyed) return;
    }
}
