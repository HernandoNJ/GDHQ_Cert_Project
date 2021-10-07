using System;
using UnityEngine;

namespace Training.Others
{
    public abstract class BaseDamage : MonoBehaviour, ITakeDamageT
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private int health;

        public virtual void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0) Destroy(gameObject);
        }

        protected virtual void SetMaxHealth(int maxHealthValue)
        {
            maxHealth = maxHealthValue;
            health = maxHealth;
        }


        private void Awake()
        {
            // modify health in inspector
            health = maxHealth;
        }



        // /// <summary>
        // /// override this method in children if you want to set the maxHealth value with hardcode - @HernandoNJ
        // /// </summary>
        // /// <param name="maxHealthValue"></param>
        // protected virtual void SetMaxHealth(int maxHealthValue)
        // {
        //     maxHealth = maxHealthValue;
        //     health = maxHealth;
        // }
    }
}