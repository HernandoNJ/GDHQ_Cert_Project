using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Animator buttonAnim;
    [SerializeField] private AudioSource buttonAudio;
    [SerializeField] private AudioSource backgroundMusic;
    private static readonly int Clicked = Animator.StringToHash("Clicked");

    private void OnEnable()
    {
        backgroundMusic = GetComponent<AudioSource>();
    }

    public void StartGame() => SceneManager.LoadScene(1);

    public void QuitGame() => Application.Quit();



}
