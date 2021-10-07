using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownCommand : ICommand
{
    private float speed;
    private Transform playerTransform;

    public MoveDownCommand(Transform playerTransform, float speed)
    {
        this.speed = speed;
        this.playerTransform = playerTransform;
    }

    public void Execute()
    {
        playerTransform.Translate(Vector3.down * (speed * Time.deltaTime));
    }

    public void Undo()
    {
        playerTransform.Translate(Vector3.up * (speed * Time.deltaTime));
    }
}