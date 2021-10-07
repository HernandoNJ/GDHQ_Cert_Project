using UnityEngine;

namespace Training.Others
{
    public class Barrel : MonoBehaviour, ITakeDamageT
    {

        public void TakeDamage(int damage)
        {
            Destroy(gameObject);
        }
    }
}