using System.Collections;
using UnityEngine;

public class FinalBossWeapons : Weapons
{
    [SerializeField] private float slowCooldown;
    [SerializeField] private float midCooldown;
    [SerializeField] private float fastCooldown;
    
    protected override void SetInitialValues()
    {
        ResetWeaponPositions();
        weaponsIndex = maxWeaponsIndex;
        UpdateWeaponPositions(weaponsIndex);
        ShootWeapons();
    }

    private void OnEnable()
    {
        Enemy.OnBossStateChanged += UpdateShootCooldown; // TODO modify cooldown
        Enemy.OnShootingStateChanged += ChangeShootingState;
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
    
    private void ShootWeapons() => StartCoroutine(nameof(ShootingRoutine));

    // Set cool down
    // FireWeapons()
    private IEnumerator ShootingRoutine()
    {
        while (gameObject.activeInHierarchy && shootEnabled)
        {
            yield return new WaitForSeconds(shootCooldown);
            // TODO shoot mechanic: instantiate lasers in firepoint 0
        }
    }
}
