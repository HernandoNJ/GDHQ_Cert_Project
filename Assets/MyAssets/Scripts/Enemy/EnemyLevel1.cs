using UnityEngine;

public class EnemyLevel1 : Enemy
{
    [SerializeField] protected bool hasPowerup;
    [SerializeField] protected int animTriggerIndex;
    
    protected override void SetInitialValues()
    {
        base.SetInitialValues();
        isEnemyLevel1 = true;
        SetEnemyVulnerable(false);
        shield.gameObject.SetActive(false);
        animTriggerIndex = enemiesSpawnerGO.GetCurrentWave();
        animController.SetTrigger("animTrigger" + (animTriggerIndex));
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "ActivateEnemiesCollider")
        {
            SetEnemyVulnerable(true);
        }
        else if (other.CompareTag("Outbound")) EnemyDestroyed();
    }

    protected override void Damage()
    {
        base.Damage();
        if (hasPowerup) Instantiate(powerupPrefab, transform.position, Quaternion.identity);
    }
}

