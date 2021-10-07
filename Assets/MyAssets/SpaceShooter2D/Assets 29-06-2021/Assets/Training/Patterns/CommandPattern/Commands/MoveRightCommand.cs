using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightCommand : ICommand
{
    private float speed;
    private Transform playerTransform;

    public MoveRightCommand(Transform playerTransform, float speed)
    {
        this.speed = speed;
        this.playerTransform = playerTransform;
    }

    public void Execute()
    {
        playerTransform.Translate(Vector3.right * (speed * Time.deltaTime));
    }

    public void Undo()
    {
        playerTransform.Translate(Vector3.left * (speed * Time.deltaTime));
    }
}