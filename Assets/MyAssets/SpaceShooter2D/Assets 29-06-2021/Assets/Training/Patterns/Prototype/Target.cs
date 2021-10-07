using Interfaces;
using UnityEngine;
using static UnityEngine.Debug;

namespace Training.Patterns.Prototype
{
public class Target : MonoBehaviour, IDamageable
{
    /* It can be anything like a player or enemy */

    [SerializeField] private float currentHealth = 10;
    //[SerializeField] private FloatingText floatingText;
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        // if(floatingText != null)
        //     floatingText.SetText("<color=red>" + damageAmount + "</color>");



    }

    public void Freeze(int seconds)
    {
        Log("Frozen for: " + seconds);
        // if (floatingText != null)
        //     floatingText.SetText("<color=blue>Frozen</color>");


    }

    public void TakeDamage(int damage)
    {

    }
}
}
