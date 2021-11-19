using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyAssets.Packs.SpShooter.Scripts
{
public class MainMenu : MonoBehaviour
{
    //load game scene
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
}
