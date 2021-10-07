using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    private static GameManager instance;
    public static GameManager Instance => instance;
    private int score;

    private string difficultyLevel;
    
    private void Awake()
    {
        instance = this;
        if(instance == null) Debug.LogError("Game Manager instance is null");
        difficultyLevel = PlayerPrefs.GetString("Difficulty");
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public string GetDifficulty()
    {
        return difficultyLevel;
    }

    public void StoreScore()
    {
        PlayerPrefs.SetInt("Score",score);
    }

    public void GameOver()
    {
        Debug.Log("Game over");
        player.SetActive(false);
    }
}
