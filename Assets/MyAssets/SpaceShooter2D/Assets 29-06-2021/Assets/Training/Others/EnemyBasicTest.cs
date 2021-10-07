using UnityEngine;

public class EnemyBasicTest : MonoBehaviour
{
    private MoveEnemy m;


    void Update()
    {
        m.MovingTheGameObject(5);
        m.MoveObject1();
        m.MoveObject2();
    }
}