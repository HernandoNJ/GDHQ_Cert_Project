using UnityEngine;
using UnityEngine.SceneManagement;

namespace Enums
{
public class LevelSelector : MonoBehaviour
    {
        public enum LevelDifficulty { Amateur = 2, Veteran = 3, Pro, Elite }

        public LevelDifficulty levelDifficulty;

        private void Start()
        {
            SceneManager.LoadScene((int)levelDifficulty);
        }
    }
}