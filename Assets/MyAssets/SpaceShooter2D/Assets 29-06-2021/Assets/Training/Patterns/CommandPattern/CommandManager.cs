using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandManager : SingletonBP<CommandManager>
{
    private List<ICommand> commandBuffer = new List<ICommand>();

    public void AddCommand(ICommand command)
    {
        commandBuffer.Add(command);
    }

    public void PlayCommands()
    {
        StartCoroutine(PlayRoutine());
    }

    public void RewindCommands()
    {
        StartCoroutine(RewindRoutine2());
    }

    private IEnumerator PlayRoutine()
    {
        foreach (var command in commandBuffer)
        {
            command.Execute();
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator RewindRoutine()
    {
        foreach (var command in Enumerable.Reverse(commandBuffer))
        {
            command.Undo();
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator RewindRoutine2()
    {
        Debug.Log("Rewinding...");
        commandBuffer.Reverse();
        foreach (var command in commandBuffer)
        {
            command.Undo();
            yield return new WaitForEndOfFrame();
        }
        commandBuffer.Reverse();
        Debug.Log("Finished rewinding");
    }

}