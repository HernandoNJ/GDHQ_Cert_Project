using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private float xPos;
    [SerializeField] private float maxUpPos;
    [SerializeField] private float minDownPos;
    [SerializeField] private int maxWavesAmount;
    [SerializeField] private int currentWave;
    [SerializeField] private int maxEnemies;
    [SerializeField] private int enemyIndex;
    [SerializeField] private int enemiesCountReference;
    [SerializeField] private bool midBossDestroyed;
    [SerializeField] private bool spawningEnabled;
    [SerializeField] private GameObject enemiesParent;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject midBossPrefab;
    [SerializeField] private GameObject finalBossPrefab;
    [SerializeField] private Transform midBossStart;
    [SerializeField] private Transform finalBossStart;
    [SerializeField] private Vector2 enemyStartPos;
    [SerializeField] private List<GameObject> enemiesList = new();
    public int EnemiesCounter { get; set; }

    private static EnemiesSpawner instance;
    public static EnemiesSpawner Instance => instance;

    private void Awake() => instance = this;

    private void Start()
    {
        // Set starting values
        CheckIfInstanceIsNull();
        currentWave = 1;
        maxWavesAmount = 13;
        EnemiesCounter = 0;
        enemyIndex = 1;
        midBossPrefab = enemiesList[8];
        finalBossPrefab = enemiesList[13];
        spawningEnabled = true;
        StartCoroutine(wavesRoutine());
    }

    private void Update()
    {
        enemiesCountReference = EnemiesCounter; // for checking enemies count value
        if (EnemiesCounter == 0) spawningEnabled = true;
        if (EnemiesCounter < 0) Debug.LogError("enemies count less than 0"); // for checking
    }

    private static void CheckIfInstanceIsNull()
    {
        if (instance == null) Debug.LogError("Enemies spawner is null");
    }

    private IEnumerator CheckForNewWaveRoutine()
    {
        spawningEnabled = false;
        while (spawningEnabled == false)
        {
            yield return new WaitForSeconds(1);
        }

        StartCoroutine(wavesRoutine());
    }

    private IEnumerator wavesRoutine()
    {
        yield return new WaitForSeconds(1);
        if (enemyIndex != 8 || enemyIndex != 13) StartCoroutine(BasicWaveRoutine());
        else if (enemyIndex == 8) StartCoroutine(MidBossRoutine());
        else if (enemyIndex == 13) StartCoroutine(FinalBossRoutine());
        currentWave++;
    }

    private IEnumerator BasicWaveRoutine()
    {
        yield return new WaitForSeconds(1);

        SetBasicEnemyValues();

        while (EnemiesCounter < maxEnemies)
        {
            Instantiate(enemyPrefab, enemyStartPos, quaternion.identity, enemiesParent.transform);
            yield return new WaitForSeconds(1f);
            EnemiesCounter++;
        }

        StartCoroutine(CheckForNewWaveRoutine());
    }

    private IEnumerator MidBossRoutine()
    {
        yield return new WaitForSeconds(2);
        Instantiate(midBossPrefab, midBossStart.position, Quaternion.identity);
        EnemiesCounter++;
        StartCoroutine(CheckForNewWaveRoutine());
    }

    private IEnumerator FinalBossRoutine()
    {
        yield return new WaitForSeconds(2);
        Instantiate(finalBossPrefab, finalBossStart.position, Quaternion.identity);
        EnemiesCounter++;
        StartCoroutine(CheckForNewWaveRoutine());
    }

    private void SetBasicEnemyValues()
    {
        var yPos = Random.Range(maxUpPos, minDownPos);
        enemyStartPos = new Vector2(xPos, yPos);
        enemyIndex = currentWave;
        enemyPrefab = enemiesList[enemyIndex];
    }
}