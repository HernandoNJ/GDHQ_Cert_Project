using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private float xPos;
    [SerializeField] private float maxUpPos;
    [SerializeField] private float minDownPos;
    [SerializeField] private GameObject enemiesParent;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] enemiesArray;

    private WaitForSeconds timeToSpawn = new WaitForSeconds(0.25f);

    private void Start()
    {
        GameManager.Instance.SetNewWave();
        SetEnemiesArray();
        StartCoroutine(SpawnEnemiesRoutine());
    }

    private void SetEnemiesArray()
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

    private IEnumerator SpawnEnemiesRoutine()
    {
        var enemyIndex = 0;

        while (enemyIndex < enemiesArray.Length)
        {
            yield return timeToSpawn;
            enemiesArray[enemyIndex].SetActive(true);
            enemyIndex++;
            
        }
    }

}
