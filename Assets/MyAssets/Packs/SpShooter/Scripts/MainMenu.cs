using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  //load game scene
  public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
