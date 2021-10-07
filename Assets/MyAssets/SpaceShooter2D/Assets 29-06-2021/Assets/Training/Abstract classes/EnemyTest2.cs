using UnityEngine;

namespace Abstracts
{
    public abstract class EnemyTest2 : MonoBehaviour
    {
        public string speed;
        public int health;
        public int gems;

        public abstract void Attack();

        public virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}
