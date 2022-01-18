using UnityEngine;

public class MidBoss : Enemy
{
    [SerializeField] private GameObject midBossPowerup;
    
    // TODO powerup
    // Check waves 
    protected override void SetInitialValues()
    {
        health = 50;
        isMidBoss = true;
        isVulnerable = false;
        animController = GetComponent<Animator>(); // modify based on state
        // Launch powerup
        var startPos = new Vector2(7, 0);
        transform.position = startPos;
        animController.Play("midBossGood");
        Invoke(nameof(EnemyVulnerable), 1f);

        SetPowerupValues();
        
        UpdateBossState(EnemyState.Good);
        
        
    }

    private void SetPowerupValues()
    {
        var powerup = powerupPrefab.GetComponent<Powerup>();
        powerup.SetHealthIncrement(10);
        powerup.SetWeaponIncrement(2);
        powerup.SetMoveEnabled(true);
    }

    private void EnemyVulnerable() => isVulnerable = true;

    
}