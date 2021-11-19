using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets.Packs.SpShooter.Scripts
{
public class UIManagerT : MonoBehaviour
{
    [SerializeField] private Text ammoText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text restartText;
    [SerializeField] private Image livesImage;
    [SerializeField] private Image thrusterBar;
    [SerializeField] private GameObject ammoCounter;
    [SerializeField] private GameObject ammoImage;
    [SerializeField] private GameObject emptyAmmoImage;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Sprite[] livesSprites;

    /* Visualize on screen the ammo count of the player in the form of current/max
    * Identify ammo Text ==> ammoText
        ? Player
        * Create 2 variables in player: totalAmmo and availableAmmo
        * Set totalAmmo value in Start

        ? UI Manager
        * Display availableAmmo/totalAmmo in ammoText 
        * Modify the info shown in ammoText ==> ammoText.text = playerAmmoCount.ToString();
        * Activate it in emptyAmmoImage
        * Resize AmmoText in Canvas
    */

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager == null) Debug.LogError("gameManager in UI Manager is null");

        scoreText.text = "Score: 0";
        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);

        OnFullAmmo();
        thrusterBar.fillAmount = 0.5f;
    }

    public void UpdateScore(int playerScore)
    {
        scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int currentLives)
    {
        livesImage.sprite = livesSprites[currentLives];

        if (currentLives == 0)
            GameOverSequence();
    }

    public void SetAmmoValues(int ammoAvailable, int AmmoTotal)
    { 
        ammoText.text = ammoAvailable.ToString() + "/" + AmmoTotal.ToString();
    }

    // public void ShowStartAmmo(int playerAmmoAvailable, int playerAmmoTotal)
    // {
    //     SetAmmoValues(playerAmmoAvailable, playerAmmoTotal);
    //     emptyAmmoImage.SetActive(false);
    //     ammoCounter.SetActive(true);
    // }

    public void UpdateAmmo(int playerAmmoAvailable, int playerAmmoTotal)
    {
        SetAmmoValues(playerAmmoAvailable, playerAmmoTotal);
    }

    public void OnEmptyAmmo()
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
        if (thrusterBar.fillAmount >= 1.0f)
            return;

        thrusterBar.fillAmount += fillAmount;
    }

    public void DecreaseThrusterBar(float emptyAmount)
    {
        if (thrusterBar.fillAmount <= 0.5f)
            return;

        thrusterBar.fillAmount -= emptyAmount;
    }

    public void GameOverSequence()
    {
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        //gameManager.OnGameOver();
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            gameOverText.text = "Game Over";
            yield return new WaitForSeconds(0.35f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.35f);
        }
    }
}
}
