using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    private static GameManager instance;
    public static GameManager Instance => instance;
    
    private int score;
    private int currentWave;
    private int currentDifficulty;
    private string difficultyLevel;
    
    // TODO increase enemies speed in difficulty
    
    private void Awake()
    {
        instance = this;
        if(instance == null) Debug.LogError("Game Manager instance is null");
        difficultyLevel = PlayerPrefs.GetString("Difficulty");
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        SetCurrentDifficulty();
    }

    public string GetDifficultyString() => difficultyLevel;
    
    private void SetCurrentDifficulty()
    {
        currentDifficulty = difficultyLevel switch
        { "Easy" => 1, "Medium" => 2, "Hard" => 3, _ => currentDifficulty };

        Debug.Log("Difficulty: " + currentDifficulty);
    }

    public int GetCurrentDifficulty() => currentDifficulty;

    public void StoreScore() => PlayerPrefs.SetInt("Score",score);

    public int SetNewWave() => currentWave++;

    public void GameOver()
    {
        Debug.Log("Game over");
        player.SetActive(false);
    }
}
