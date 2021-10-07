using UnityEngine;
using static UnityEngine.Debug;

namespace Events
{
    public class GameManager : MonoBehaviour
    {
        private void OnEnable()
        {
            Player.OnDeath += ResetPlayer;

        }
        public void ResetPlayer() => Log("Reset player");

        private void OnDisable()
        {
            Player.OnDeath -= ResetPlayer;
        }

    }
}
