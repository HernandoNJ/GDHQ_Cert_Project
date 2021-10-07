using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Debug;

namespace Events
{
    public class UIManager : MonoBehaviour
    {
        public int deathCounter;
        public Text deathCountText;

        private void OnEnable()
        {
            Player.OnDeath += UpdateDeathCount;
            Player.OnDamageReceived += UpdateHealth;
        }

        public void UpdateHealth(int health)
        {
            Log("Current health: " + health);
        }

        public void UpdateDeathCount()
        {
            deathCounter++;
            deathCountText.text = "Death count: " + deathCounter;
        }

        private void OnDisable()
        {
            Player.OnDeath -= UpdateDeathCount;
            Player.OnDamageReceived -= UpdateHealth;
        }

    }
}