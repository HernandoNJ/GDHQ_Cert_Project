using System.Collections;
using UnityEngine;

public class EnemyLevel1Weapons : Weapons
{
    [SerializeField] private float waitForShooting;
    
    private void OnEnable()
    {
        Enemy.OnShootingStateChanged += UpdateShootingState;
    }

    private void OnDisable()
    {
        Enemy.OnShootingStateChanged -= UpdateShootingState;
        StopAllCoroutines();
    }

    private void UpdateShootingState(bool isShootEnabled)
    {
        if (isShootEnabled)
        {
            ChangeShootingState(true);
            StartCoroutine(nameof(WaitToStartShootingRoutine));
        }
        else ChangeShootingState(false);
    }

    protected override void FireWeapons()
    {
        base.FireWeapons();
        Instantiate(weaponsPrefabs[weaponsIndex], weaponsPositions[0].transform.position, Quaternion.identity);
    }

    private IEnumerator WaitToStartShootingRoutine()
    {
        yield return new WaitForSeconds(waitForShooting);
        StartCoroutine(nameof(ShootingRoutine));
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
