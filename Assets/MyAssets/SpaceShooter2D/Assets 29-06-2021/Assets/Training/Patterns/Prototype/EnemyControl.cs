using EnemyNS;
using UnityEngine;

namespace Training.Patterns.Prototype
{
public class EnemyControl : MonoBehaviour
{
    public EnemyData data;
    public float currentHealth;

    private void Start()
    {
        
    }

    // private void Update()
    // {
    //     MoveEnemy();
    // }

    // public void MoveEnemy()
    // {
    //     if (data.enemyName == "Basic") data.MoveBasic();
    //     if (data.enemyName == "Chaser") data.MoveChaser();
    //     if (data.enemyName == "Aggressive") data.MoveAggressive();
    // }
}
}

// public void FireWeapon(int firingRate)
//     {
//     }

//     public void Move(float moveSpeed)
//     {
//     }

//     public void Shield(int shieldForce)
//     {
//     }

//     public void TakeDamage(int damage)
//     {
//     }




// }

// enemyModel.transform.SetParent(this.transform);
// enemyModel.transform.localPosition = Vector3.zero;
// enemyModel.transform.rotation = Quaternion.identity;

// this.speed = data.speed;


// private void Update()
// {
//     transform.Translate(Vector3.down * speed * Time.deltaTime);
// }



