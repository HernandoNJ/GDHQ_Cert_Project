using UnityEngine;

public class MoveEnemy : MonoBehaviour, IMoveInterface
{


    public void MovingTheGameObject(int speed)
        => transform.Translate(Vector3.down * (speed * Time.deltaTime));

    public void MovingOtherObject(int speed){ }

    public void MoveObject1(){ }

    public void MoveObject2(){ }
}