using System.Collections;
using UnityEngine;

public class EnemyLevel1Weapons : Weapons
{
    [SerializeField] private bool shootEnabled;
    
    private void OnEnable()
    {
        Enemy.OnShootingStateChanged += ChangeShootingState;
    }

    private void OnDisable()
    {
        Enemy.OnShootingStateChanged -= ChangeShootingState;
        StopCoroutine(nameof(ShootingRoutine));
    }
    
    protected override void Start()
    {
        base.Start();
        ChangeShootingState(true);
        StartCoroutine(nameof(ShootingRoutine));
    }
    
    private void ChangeShootingState(bool shootEnabledArg)
    {
        shootEnabled = shootEnabledArg;
    }

    protected override void FireWeapons()
    {
        base.FireWeapons();
        Instantiate(weaponsPrefabs[weaponsIndex], weaponsPositions[0].transform.position, Quaternion.identity);
    }

    private IEnumerator ShootingRoutine()
    {
        while (gameObject.activeInHierarchy && shootEnabled)
        {
            FireWeapons();
            yield return new WaitForSeconds(shootCooldown);
        }
    }
}
