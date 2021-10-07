using System.Collections;
using EnemyNS;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject ammoImage;
    [SerializeField] private GameObject emptyAmmoImage;
    [SerializeField] private Image livesImage;
    [SerializeField] private Image thrusterBar;
    [SerializeField] private Text ammoText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text restartText;
    [SerializeField] private Sprite[] livesSprites;
    [SerializeField] private int playerScore;

    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null) Debug.Log("UIManager is null");
            return _instance;
        }
    }

    private void Awake() => _instance = this;

    private void OnEnable()
    { 
        //WeaponsManager.OnAmmoChanged += UpdateAmmoCounter;
        //PlayerHealth.OnHealthChanged += UpdateLives;
        //PlayerHealth.OnPlayerDestroyed += GameOverSequence;
        EnemyBase.OnEnemyDestroyed += UpdateScore;
    }

    private void OnDisable()
    {
        //WeaponsManager.OnAmmoChanged -= UpdateAmmoCounter;
        //PlayerHealth.OnHealthChanged -= UpdateLives;
        //PlayerHealth.OnPlayerDestroyed -= GameOverSequence;
        EnemyBase.OnEnemyDestroyed -= UpdateScore;
    }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager == null) Debug.LogError("gameManager in UI Manager is null");

        playerScore = 0;
        scoreText.text = "Score: " + playerScore;
        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);

        UpdateLives(3);
        OnFullAmmo();
        thrusterBar.fillAmount = 0.5f;
    }

    private void UpdateScore()
    {
        playerScore++;
        scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int currentLives)
    {
        livesImage.sprite = livesSprites[currentLives];
    }

    private void SetAmmoValues(int ammoAvailable, int ammoTotal)
    {
        ammoText.text = ammoAvailable.ToString() + "/" + ammoTotal.ToString();
    }

    private void UpdateAmmoCounter(int playerAmmoAvailable, int playerAmmoTotal)
    {
        SetAmmoValues(playerAmmoAvailable, playerAmmoTotal);
        if (playerAmmoAvailable <= 0) OnEmptyAmmo();
        if (playerAmmoAvailable == playerAmmoTotal) OnFullAmmo();
    }

    private void OnEmptyAmmo()
    {
        emptyAmmoImage.SetActive(true);
        emptyAmmoImage.GetComponent<Animator>().SetBool("isAmmoEmpty", true);
        ammoImage.SetActive(false);
    }

    public void OnFullAmmo()
    {
        emptyAmmoImage.SetActive(false);
        ammoImage.SetActive(true);
    }

    public void IncreaseThrusterBar(float fillAmount)
    {
        if (thrusterBar.fillAmount >= 1.0f) return;
        thrusterBar.fillAmount += fillAmount;
    }

    public void DecreaseThrusterBar(float emptyAmount)
    {
        if (thrusterBar.fillAmount <= 0.5f) return;
        thrusterBar.fillAmount -= emptyAmount;
    }

    private void GameOverSequence()
    {
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        gameManager.SetGameOver();
    }

    private IEnumerator GameOverFlickerRoutine()
    {
        var showMessageTime = Time.time + 5f;
        while (showMessageTime > Time.time)
        {
            gameOverText.text = "Game Over";
            yield return new WaitForSeconds(0.35f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.35f);
        }
    }
}
}
