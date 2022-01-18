using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hudWarning;
    [SerializeField] private Image[] livesArray;
    [SerializeField] private int scoreValue;
    [SerializeField] private int livesAmount;

    private static UIManager instance;
    public static UIManager Instance => instance;

    private void Awake() => instance = this;
    
    private void Start()
    {
        UpdateScore(scoreValue);
        ActivateFullLives();
    }

    private void ActivateFullLives()
    {
        foreach (var img in livesArray) { img.gameObject.SetActive(true); }
    }
    
    public int LivesAmount { set => livesAmount = value; } 

    public void ReduceLives(int value)
    {
        livesAmount -= value;
        livesArray[livesAmount].gameObject.SetActive(false);
    }

    public void UpdateScore(int value)
    {
        scoreValue = value;
        scoreText.text = "Score: " + scoreValue;
    }


}
