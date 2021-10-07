using UnityEngine;
using static UnityEngine.Debug;

namespace Abstracts
{
    public class MossGiant : EnemyTest2
    {
        private void Start()
        {
            Die();
        }

        override public void Attack()
        {

        }

        public override void Die()
        {
            // Add custom particle system
            base.Die();
            Log("Gameobject is destroyed");
            Log("GameObject name: " + gameObject.name);
        }
    }
}