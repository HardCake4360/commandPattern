using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    private Queue<TimedCommand> commandHistory = new Queue<TimedCommand>();
    private float startTime;
    public int maxSize;

    public void StartRecording()
    {
        startTime = Time.time;
        commandHistory.Clear();
    }

    public void ExecuteCommand(ICommand command)
    {
        float timeStamp = Time.time - startTime;
        var timedCommand = new TimedCommand(command, timeStamp);

        if(commandHistory.Count >= maxSize)
        {
            commandHistory.Dequeue();
        }

        command.Execute();
        commandHistory.Enqueue(timedCommand);
    }

    public void ReplayCommands(MonoBehaviour context)
    {
        context.StartCoroutine(ReplayCoroutine());
    }

    public IEnumerator ReplayCoroutine()
    {
        if (commandHistory.Count == 0)
            yield break;

        TimedCommand[] commandsArray = commandHistory.ToArray();

        float previousTime = 0f;

        foreach (var timedCommand in commandsArray)
        {
            float delay = timedCommand.Timestamp - previousTime;
            yield return new WaitForSeconds(delay);
            timedCommand.Command.Execute();
            previousTime = timedCommand.Timestamp;
        }
    }

    public void ClearHistory()
    {
        commandHistory.Clear();
    }
}