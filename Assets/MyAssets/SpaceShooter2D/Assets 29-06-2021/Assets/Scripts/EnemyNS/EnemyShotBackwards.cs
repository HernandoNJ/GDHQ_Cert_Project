using System.Collections;
using UnityEngine;

namespace EnemyNS
{
public class EnemyShotBackwards : EnemyBase
{
    [SerializeField] private bool shootBackActive;
    [SerializeField] private GameObject backwardsLaser;
    [SerializeField] private Transform scannerPoint;
    [SerializeField] private LayerMask playerMask;

    protected override void ConfigureEnemy(EnemyData _data)
    {
        base.ConfigureEnemy(_data);
        shootBackActive = true;
    }

    protected override void FireWeapon()
    {
        base.FireWeapon();
        LookPlayerBackwards();
    }

    private void LookPlayerBackwards()
    {
        Debug.DrawRay(scannerPoint.position, Vector3.up * 3f, Color.cyan);

        var rayHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 10f, playerMask);
        var rayHitCol = rayHit.collider;

        if (rayHitCol != null)
        {
            if (rayHitCol.CompareTag("Player"))
            {
                if (shootBackActive)
                {
                    StartCoroutine(ShootBackwardsRoutine());
                }
            }
        }
    }

    private IEnumerator ShootBackwardsRoutine()
    {
        shootBackActive = false;
        Instantiate(backwardsLaser, scannerPoint);
        yield return new WaitForSeconds(2);
        shootBackActive = true;
    }
}
}
