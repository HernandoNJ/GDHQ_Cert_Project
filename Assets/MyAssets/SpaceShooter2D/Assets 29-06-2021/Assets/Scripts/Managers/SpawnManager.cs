using System;
using System.Collections;
using System.Collections.Generic;
using Starting;
using UnityEngine;
using ScriptableObjects.Waves;
using Random = UnityEngine.Random;

namespace Managers
{
public class SpawnManager : SingletonBP<SpawnManager>
{
    [SerializeField] private int randomPowerup;
    [SerializeField] private int currentWaveIndex;
    [SerializeField] private int currentWaveNumber;
    [SerializeField] private GameObject[] powerups;
    [SerializeField] private List<WaveData> waves = new List<WaveData>();
    [SerializeField] private Transform enemySpawner;
    [SerializeField] private bool isPlayerAlive;

    public static event Action<int> OnWaveStarted;

    private void OnEnable()
    {
        Asteroid.OnAsteroidDestroyed += StartSpawning;
        //Player.OnPlayerActive += SetPlayerAlive;
        //PlayerHealth.OnPlayerDestroyed += SetPlayerDestroyed;
        PlayerStart.onPlayerDestroyed += SetPlayerDestroyed;
    }

    private void OnDisable()
    {
        Asteroid.OnAsteroidDestroyed -= StartSpawning;
       //Player.OnPlayerActive -= SetPlayerAlive;
        //PlayerHealth.OnPlayerDestroyed -= SetPlayerDestroyed;
        PlayerStart.onPlayerDestroyed -= SetPlayerDestroyed;
    }

    private void StartSpawning()
    {
        StartCoroutine(EnemyWaveRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnMultipleShotRoutine());
    }

    private void SetPlayerAlive()
    {
        isPlayerAlive = true;
    }

    public void SetPlayerDestroyed()
    {
        isPlayerAlive = false;
    }

    private IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (isPlayerAlive)
        {
            var powerupPos = new Vector2(Random.Range(-9.5f, 9.5f), 5f);
            randomPowerup = Random.Range(0, 6);
            Instantiate(powerups[randomPowerup], powerupPos, Quaternion.identity);
            switch (randomPowerup)
            {
                case 2:
                case 4:
                    yield return new WaitForSeconds(Random.Range(4, 7) * (currentWaveNumber * 1.2f));
                    break;
                case 0:
                case 3:
                    yield return new WaitForSeconds(Random.Range(4, 7) * (currentWaveNumber / 1.2f));
                    break;
                default:
                    yield return new WaitForSeconds(Random.Range(4, 7));
                    break;
            }
        }
    }

    private IEnumerator SpawnMultipleShotRoutine()
    {
        yield return new WaitForSeconds(5);
        while (isPlayerAlive)
        {
            var powerupPos = new Vector2(Random.Range(-9.5f, 9.5f), 5f);
            Instantiate(powerups[6], powerupPos, Quaternion.identity);
            yield return new WaitForSeconds(7);
        }
    }

    private IEnumerator EnemyWaveRoutine()
    {
        yield return new WaitForSeconds(2);

        while (isPlayerAlive)
        {
            currentWaveNumber = currentWaveIndex + 1;
            OnWaveStarted?.Invoke(currentWaveNumber);
            var currentWave = waves[currentWaveIndex].waveGameObjects; // Get objects list to instantiate
            var previousWave =
                new GameObject("PreviousWave"); // Create a new parent to be removed after wave is finished
            previousWave.transform.SetParent(enemySpawner);

            foreach (var obj in currentWave)
            {
                var newEnemy = Instantiate(obj, previousWave.transform); // Instantiate obj in PreviousWave
                newEnemy.transform.position = new Vector2(Random.Range(-9.5f, 9.5f), 5f);
                yield return new WaitForSeconds(Random.Range(2, 5));
            }

            yield return new WaitForSeconds(5); // wait 5 seconds after wave is done
            Destroy(previousWave); // clear up wave objects
            currentWaveIndex++;

            if (currentWaveIndex != waves.Count) continue;
            break; // get out from while loop
        }
    }
}
}
