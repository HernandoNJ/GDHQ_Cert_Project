using EnemyNS;
using UnityEngine;

public class EnemyShielded : EnemyBase
{
    [SerializeField] private GameObject enemyShield;

    public override void TakeDamage(int damage)
    {
        if (enemyShield.activeInHierarchy)
        {
            currentHealth--;
            enemyShield.SetActive(false);
            return;
        }
        base.TakeDamage(damage);
    }
}
