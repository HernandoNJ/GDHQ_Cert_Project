using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int score;
    [SerializeField] private int currentDifficulty;
    [SerializeField] private string difficultyLevel;
    [SerializeField] private bool isGameOver; // TODO just for testing
    
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public static event Action OnGameStarted;
    public static event Action OnGameOver;
    
    private void Awake()
    {
        _instance = this;
        if(_instance == null) Debug.LogError("Game Manager instance is null");
        difficultyLevel = PlayerPrefs.GetString("Difficulty");
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDestroyed += PlayerScored;
        Enemy.OnFinalBossDestroyed += FinalBossDestroyed;
    }
    
    private void OnDisable()
    {
        Enemy.OnEnemyDestroyed -= PlayerScored;
        Enemy.OnFinalBossDestroyed -= FinalBossDestroyed;
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        OnGameStarted?.Invoke();
    }
    
    public void PlayerScored(int scoreValueArg)
    {
        score += scoreValueArg;
        UIManager.Instance.UpdateScore(score);
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
