using UnityEngine;

namespace EnemyNS
{
public class EnemyAggressive : EnemyBase
{
    [SerializeField] private Transform target;
    [SerializeField] private int xFlip = 1;
    [SerializeField] private float rotateValue = 1;
    [SerializeField] private float maxRotation = 40f;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private bool playerFound;

    /* Physics Raycast Unity
     https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
     */

    protected override void ConfigureEnemy(EnemyData _data)
    {
        base.ConfigureEnemy(_data);
        playerMask = 1 << 14;
    }

    protected override void MoveEnemy()
    {
        RamWithRaycast();
    }

    private void RamWithRaycast()
    {
        ScanForPlayer();

        if (playerFound)
        {
            LookAtTarget();
            MoveToTarget();
        }
        else
        {
            MoveDownGlobal();
            RotateEnemy();
        }

        CheckBottomPosition();
    }

    private void ScanForPlayer()
    {
        var position = transform.position;

        var playerHit2D = Physics2D.OverlapCircle(position, 3f, playerMask);

        if (playerHit2D != null)
        {
            playerFound = true;
            target = playerHit2D.transform;

            Debug.DrawRay(position, target.position - position, Color.yellow);
        }
        else
        {
            playerFound = false;
            var drawRayDir = transform.TransformDirection(Vector2.down);
            Debug.DrawRay(position, drawRayDir * 7f, Color.white);
            Debug.Log(" did not hit");
        }
    }

    private void RotateEnemy()
    {
        rotateValue += xFlip;
        if (rotateValue > maxRotation) xFlip = -1;
        if (rotateValue < -maxRotation) xFlip = 1;
        transform.rotation = Quaternion.Euler(0, 0, rotateValue);
    }

    private void MoveDownGlobal()
    {
        var moveDown = transform.InverseTransformDirection(Vector2.down);
        transform.Translate(moveDown * (enemySpeed * Time.deltaTime));
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
    }

    private void LookAtTarget()
    {
        transform.up = -(target.position - transform.position);
    }
}
}
