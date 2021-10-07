using UnityEngine;

public class MoveUpCommand : ICommand
{
    private Transform playerTransform;
    private float speed;

    public MoveUpCommand(Transform playerTransform, float speed )
    {
        this.playerTransform = playerTransform;
        this.speed = speed;
    }

    public void Execute()
    {
        playerTransform.Translate(Vector3.up * (speed * Time.deltaTime));
    }

    public void Undo()
    {
        playerTransform.Translate(Vector3.down * (speed * Time.deltaTime));
    }
}