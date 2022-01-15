using System;
using UnityEngine;

public class MidBoss : Enemy
{
    [SerializeField] private GameObject powerup;

    public static event Action OnMidBossWaveStarted; 

    // TODO powerup
    // TODO check out set initial values()
    // Check waves 
    protected override void SetInitialValues()
    {
        OnMidBossWaveStarted?.Invoke();
        
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
        
        // TODO check out 
        //StartCoroutine(ShootingRoutine()); 
    }

    private void SetPowerupValues()
    {
        var powerup = powerupPrefab.GetComponent<Powerup>();
        powerup.SetHealthIncrement(10);
        powerup.SetWeaponIncrement(2);
        powerup.SetMoveEnabled(true);
    }

    private void EnemyVulnerable() => isVulnerable = true;

    // TODO check here and below
    // protected virtual IEnumerator ShootingRoutine()
    // {
    //
    //     //while (gameObject.activeInHierarchy && shootEnabled)
    //     {
    //         //yield return new WaitForSeconds(shootCooldown);
    //         // TODO shoot mechanic: instantiate lasers in firepoint 0
    //     }
    // }
    // protected override IEnumerator ShootingRoutine()
    // {
    //     while (gameObject.activeInHierarchy && playerDestroyed == false)
    //     {
    //         for (float i = 0; i < 0.5f; i += 0.1f)
    //         {
    //             yield return new WaitForSeconds(shootCooldown);
    //             Shoot();
    //         }
    //
    //         yield return new WaitForSeconds(1.5f);
    //     }
    //     
    //     animController.Play("Empty");
    // }
}