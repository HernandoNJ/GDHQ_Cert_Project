using UnityEngine;

namespace Training.Patterns.CommandPattern
{
public class PlayerInput : MonoBehaviour
{
    /* Steps to create commands
     * Create an interface
     * Create a Command class for each command
     * Define what you want, for example, to get a reference to the player and his speed
     * Create a command's constructor to communicate with player
     * Define what you want to do when a method like Execute() in MoveUpCommand is called, for example, Translate using Vector3.up *  speed * T.dT
     * Create and execute a command for each player input
     * Create a CommandManager
     *
     */


    private ICommand moveUp, moveDown, moveLeft, moveRight;

    [SerializeField] private float speed = 5f;

    void Start() {  }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            moveUp = new MoveUpCommand(transform, speed);
            SetUpCommand(moveUp);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDown = new MoveDownCommand(transform, speed);
            SetUpCommand(moveDown);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveLeft = new MoveLeftCommand(transform, speed);
            SetUpCommand(moveLeft);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveRight = new MoveRightCommand(transform, speed);
            SetUpCommand(moveRight);
        }
    }

    private void SetUpCommand(ICommand iCommand)
    {
        iCommand.Execute();
        CommandManager.Instance.AddCommand(iCommand);
    }

}
}