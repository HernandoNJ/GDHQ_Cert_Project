using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
public class GameManager : MonoBehaviour
{
    [SerializeField] private bool isGameOver;

    public void SetGameOver()
    {
        isGameOver = true;
    }
}
}
