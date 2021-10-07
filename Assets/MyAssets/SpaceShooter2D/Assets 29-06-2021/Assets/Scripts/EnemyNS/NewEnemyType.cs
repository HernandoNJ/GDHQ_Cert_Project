using UnityEngine;

namespace EnemyNS
{
public class NewEnemyType : EnemyBase
{
    [SerializeField] private int flipXMove = 1;

    protected override void MoveEnemy()
    {
        transform.position += (Vector3.right * flipXMove + Vector3.down/10f) * (enemySpeed * 2 * Time.deltaTime);

        if (transform.position.x > 7.5f)flipXMove = -1;
        if (transform.position.x < -7.5f)flipXMove = 1;

        CheckBottomPosition();
    }
}
}
