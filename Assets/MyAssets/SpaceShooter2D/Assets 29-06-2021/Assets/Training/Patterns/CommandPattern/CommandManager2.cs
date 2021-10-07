using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandManager2 : SingletonBP<CommandManager2>
{
    // List for storing all commands created
    private List<ICommand2> commandBuffer = new List<ICommand2>();

    /*
     * Method to add commands
     * Routine triggered by a Play method to play back all commands - 1 sec delay
     * Routine triggered by a Rewind method to play back all commands in reverse - 1 sec delay
     * Done = Turn all cubes white
     * Reset - Clear command buffer
     */

    public void AddCommands(ICommand2 command2) => commandBuffer.Add(command2);

    public void PlayCommands() => StartCoroutine(PlayCommandsRoutine());

    public void RewindCommands() => StartCoroutine(RewindCommandsRoutine());

    public void DoneCommands()
    {
        var cubes = GameObject.FindGameObjectsWithTag("Cube");

        foreach (var cube in cubes)
            cube.GetComponent<Renderer>().material.color = Color.white;
    }

    public void ResetCommands() => commandBuffer.Clear();

    private IEnumerator PlayCommandsRoutine()
    {
        Debug.Log("Play routine init");

        foreach (var command in commandBuffer)
        { command.ShowCubeColor(); yield return new WaitForSeconds(1); }

        Debug.Log("Commands amount: " + commandBuffer.Count);
        Debug.Log("Exiting  play routine");
    }

    private IEnumerator RewindCommandsRoutine()
    {
        Debug.Log("rewind routine init");

        foreach (var command in Enumerable.Reverse(commandBuffer))
        { command.ShowCubeColor(); yield return new WaitForSeconds(1); }

        Debug.Log("Commands amount: " + commandBuffer.Count);
        Debug.Log("exiting rewind routine");
    }
}