using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private int score;
    private int enemiesCount;
    private int currentDifficulty;
    private string difficultyLevel;
    private bool isGameOver;
    
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private EnemiesSpawner enemiesSpawner;

    public static event Action OnGameOver;
    
    private void Awake()
    {
        _instance = this;
        if(_instance == null) Debug.LogError("Game Manager instance is null");
        difficultyLevel = PlayerPrefs.GetString("Difficulty");
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        SetCurrentDifficulty();
        enemiesSpawner = EnemiesSpawner.Instance;
    }

    private void UpdateEnemyCount(int enemyAmount)
    {
        enemiesCount += enemyAmount;
       
        if(enemiesCount == 0) 
            enemiesSpawner.StartNewWave();
    }

    public int GetEnemiesAmount()
    {
        return enemiesCount;
    }

    public void OnEnemyCreated(int enemiesAmount)
    {
        UpdateEnemyCount(enemiesAmount);
    }
    
    public void OnEnemyDestroyed(int enemyAmount)
    {
        UpdateEnemyCount(enemyAmount);
    }
    
    public void OnPlayerScored(int enemyAmount, int scoreValue)
    {
        UpdateEnemyCount(enemyAmount);
        UIManager.Instance.UpdateScore(scoreValue);
    }

    private void SetCurrentDifficulty()
    {
        currentDifficulty = difficultyLevel switch
        { "Easy" => 1, "Medium" => 2, "Hard" => 3, _ => currentDifficulty };

        Debug.Log("Difficulty: " + currentDifficulty);
    }

    // TODO add to Game Over or Quit functions
    // TODO create Game UI
    
    private void StoreScore()
    {
        PlayerPrefs.SetInt("Score", score);
    }

    public void FinalBossDestroyed()
    {
        Debug.LogWarning("Final boss destroyed");
        GameOver();
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
        Debug.Log("Game over");
        StoreScore();
        player.SetActive(false);
    }
}
