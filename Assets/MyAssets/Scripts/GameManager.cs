using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    private static GameManager instance;
    public static GameManager Instance => instance;
    
    private int score;
    private int currentDifficulty;
    private string difficultyLevel;
    public bool finalBossDestroyed;

    private void Awake()
    {
        instance = this;
        if(instance == null) Debug.LogError("Game Manager instance is null");
        difficultyLevel = PlayerPrefs.GetString("Difficulty");
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        Enemy.OnFinalBossDestroyed += FinalBossDestroyed;
        SetCurrentDifficulty();
    }


    private void SetCurrentDifficulty()
    {
        currentDifficulty = difficultyLevel switch
        { "Easy" => 1, "Medium" => 2, "Hard" => 3, _ => currentDifficulty };

        Debug.Log("Difficulty: " + currentDifficulty);
    }

    public int GetCurrentDifficulty() => currentDifficulty;

    private void StoreScore()
    {
        PlayerPrefs.SetInt("Score", score);
    }

    public void FinalBossDestroyed()
    {
        Debug.LogWarning("Final boss destroyed");
    }

    public void GameOver()
    {
        Debug.Log("Game over");
        StoreScore();
        player.SetActive(false);
    }
}
