using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private float xPos;
    [SerializeField] private float maxUpPos;
    [SerializeField] private float minDownPos;
    [SerializeField] private int currentWave;
    [SerializeField] private int enemiesWaveAmount;
    [SerializeField] private bool midBossDestroyed;
    [SerializeField] private bool finalBossDestroyed;
    [SerializeField] private GameObject enemiesParent;
    [SerializeField] private GameObject enemyPrefab;
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
        StartPhase(currentWave);
    }

    private static void CheckIfInstanceIsNull()
    {
        if (instance == null) Debug.LogError("Enemies spawner is null");
    }

    private void StartPhase(int value)
    {
        if (value <= 0 || value > 4)
        {
            Debug.LogError("phase value not valid");
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        switch (value)
        {
            case 1:
                StartCoroutine(SpawnEnemiesRoutine());
                break;
            case 2:
                InstantiateMidBoss();
                break;
            case 3:
                StartCoroutine(SpawnEnemiesRoutine());
                break;
            case 4:
                InstantiateFinalBoss();
                break;
        }
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

    public void MidBossDestroyed()
    {
        midBossDestroyed = true;
        currentWave++;
        StartPhase(currentWave);
    }

    public void FinalBossDestroyed()
    {
        finalBossDestroyed = true;
        GameManager.Instance.FinalBossDestroyed();
    }

    private void InstantiateMidBoss()
    {
        Instantiate(midBossPrefab, midBossPos.position, Quaternion.identity);
    }

    private void InstantiateFinalBoss()
    {
        Instantiate(finalBossPrefab, finalBossStartPos.position, Quaternion.identity);
    }
    
    // Phases 1 and 3
    private IEnumerator SpawnEnemiesRoutine()
    {
        var enemiesAmount = 0;

        while (enemiesAmount < enemiesWaveAmount)
        {
            enemiesArray[enemiesAmount].SetActive(true);
            enemiesAmount++;
            yield return timeToNextSpawn;
        }

        currentWave++;
        StartPhase(currentWave);
    }

    // Remove after asking
    private IEnumerator WavesRoutine()
    {
        yield return timeToNextWave;

        for (currentWave = 1; currentWave < enemiesWaveAmount; currentWave++)
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
