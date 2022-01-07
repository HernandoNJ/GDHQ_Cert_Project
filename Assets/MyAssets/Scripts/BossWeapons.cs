using System.Collections;
using UnityEngine;

public class BossWeapons : Weapons
{
    [SerializeField] private bool shootEnabled;
    [SerializeField] private float slowCooldown;
    [SerializeField] private float midCooldown;
    [SerializeField] private float fastCooldown;
    
    protected override void Start()
    {
        base.Start();
        shootEnabled = true;
        StartCoroutine(nameof(ShootingRoutine));
    }

    private void OnEnable()
    {
        Enemy.OnBossStateChanged += UpdateShootCooldown; // TODO modify cooldown
        Enemy.OnShootingStateChanged += ChangeShootingState;
    }

    private void ChangeShootingState(bool shootEnabledArg)
    {
        shootEnabled = shootEnabledArg;
    }

    private void OnDisable()
    {
        Enemy.OnBossStateChanged -= UpdateShootCooldown;
        StopCoroutine(nameof(ShootingRoutine));
    }

    private void UpdateShootCooldown(Enemy.EnemyState enemyStateArg)
    {
        switch (enemyStateArg)
        {
            case Enemy.EnemyState.Good:
                shootCooldown = slowCooldown;
                weaponsIndex = 0;
                break;
            case Enemy.EnemyState.Regular:
                shootCooldown = midCooldown;
                weaponsIndex = 1;
                break;
            case Enemy.EnemyState.Bad:
                shootCooldown = fastCooldown;
                weaponsIndex = 2;
                break;
            default: 
                Debug.LogWarning("Set a valid cooldown value");
                break;
        }
        
        UpdateWeaponPositions(weaponsIndex);
    }
    
    // Set cool down
    // FireWeapons()

    protected virtual IEnumerator ShootingRoutine()
    {
        while (gameObject.activeInHierarchy && shootEnabled)
        {
            yield return new WaitForSeconds(shootCooldown);
            // TODO shoot mechanic: instantiate lasers in firepoint 0
        }
    }
}
