using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int enemiesCount;
    [SerializeField] private int score;
    [SerializeField] private int currentDifficulty;
    [SerializeField] private string difficultyLevel;
    [SerializeField] private bool isGameOver; // TODO just for testing
    
    private EnemiesSpawner enemiesSpawner;
    
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public static event Action OnGameOver;
    
    private void Awake()
    {
        _instance = this;
        if(_instance == null) Debug.LogError("Game Manager instance is null");
        difficultyLevel = PlayerPrefs.GetString("Difficulty");
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDestroyed += UpdateEnemiesCountScore;
        Enemy.OnFinalBossDestroyed += FinalBossDestroyed;
    }
    
    private void OnDisable()
    {
        Enemy.OnEnemyDestroyed -= UpdateEnemiesCountScore;
        Enemy.OnFinalBossDestroyed -= FinalBossDestroyed;
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemiesSpawner = EnemiesSpawner.Instance;
    }

    private void UpdateEnemyCount(int enemyAmount)
    {
        enemiesCount += enemyAmount;
       
        if(enemiesCount == 0) 
            enemiesSpawner.StartNewWave();
    }
    
    // TODO verify with events

    public void OnEnemyCreated(int enemiesAmount)
    {
        UpdateEnemyCount(enemiesAmount);
    }
    
    public void OnEnemyDestroyed(int enemyAmount)
    {
        UpdateEnemyCount(enemyAmount);
    }

    private void UpdateEnemiesCountScore(int n, int scoreValueArg)
    {
        UpdateEnemyCount(-n);
        score += scoreValueArg;
        UIManager.Instance.UpdateScore(score);
    }
    
    public void PlayerScored(int enemyAmount, int scoreValue)
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

    private void FinalBossDestroyed()
    {
        Debug.LogWarning("Final boss destroyed"); // TODO just for testing
        Debug.Log("PLAYER WINS"); // TODO just for testing
        GameOver();
    }

    public void GameOver()
    {
        Debug.Log("Game over"); // TODO just for testing
        OnGameOver?.Invoke();
        isGameOver = true; // TODO just for testing
        StoreScore();
        player.SetActive(false);
        Time.timeScale = 0.4f;
    }
}
