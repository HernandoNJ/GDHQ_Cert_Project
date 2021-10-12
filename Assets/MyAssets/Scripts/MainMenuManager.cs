using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundAudio;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Slider backgroundAudioSlider;
    [SerializeField] private Light uiLight;

    private int playerScore;
    
    private static readonly int Clicked = Animator.StringToHash("Clicked");
    private readonly WaitForSeconds buttonAnimWait = new WaitForSeconds(0.3f);

    private void Start()
    {
        backgroundAudioSlider.value = 0.1f;
        brightnessSlider.value = 1f;
        SetScoreValue();
    }

    private void Update()
    {
        SetBrightnessValue();
        SetAudioBackground();
    }

    public void SetScoreValue()
    {
        playerScore = PlayerPrefs.GetInt("Score");
    }

    public void EasyLevelToggle()
    {
        PlayerPrefs.SetString("Difficulty","Easy");
    }
    
    public void MediumLevelToggle()
    {
        PlayerPrefs.SetString("Difficulty","Medium");
    }
    
    public void HardLevelToggle()
    {
        PlayerPrefs.SetString("Difficulty","Hard");
    }

    private void SetAudioBackground()
    {
        backgroundAudio.volume = backgroundAudioSlider.value;
    }

    private void SetBrightnessValue()
    {
        uiLight.intensity = brightnessSlider.value;
    }

    public void StartNewGame(int sceneIndex)
    {
        StartCoroutine(StartGameRoutine(sceneIndex));
    }
    
    public void StoreScore()
    {
        PlayerPrefs.SetInt("Score", playerScore);
    }

    public void QuitGame()
    {
        StoreScore();
        StartCoroutine(QuitGameRoutine());
    }

    public void SettingsMainMenu()
    {
        StartCoroutine(SettingsRoutine());
    }

    private IEnumerator StartGameRoutine(int sceneIndex)
    {
        yield return buttonAnimWait;
        SceneManager.LoadScene(sceneIndex);
    }
    
    private IEnumerator QuitGameRoutine()
    {
        yield return buttonAnimWait;
        Application.Quit();
    }

    private IEnumerator SettingsRoutine()
    {
        yield return buttonAnimWait;
        settingsMenu.SetActive(true);
    }
}
