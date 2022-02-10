using UnityEngine;

public class EnemyLevel1 : Enemy
{
    [SerializeField] protected int animTriggerIndex;
    
    protected override void SetInitialValues()
    {
        base.SetInitialValues();
       // hasPowerup = Random.Range(0f, 1f) > 0.5f;
       // animTriggerIndex = enemiesSpawnerGO.GetCurrentWave();
       
        animController.SetTrigger("animTrigger" + animTriggerIndex);
    }
}

