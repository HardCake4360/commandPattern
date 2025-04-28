using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    private List<TimedCommand> commandHistory = new List<TimedCommand>();
    private float startTime;

    public void StartRecording()
    {
        startTime = Time.time;
        commandHistory.Clear();
    }

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        float timeStamp = Time.time - startTime;
        commandHistory.Add(new TimedCommand(command,timeStamp));
    }

    public void ReplayCommands(MonoBehaviour context)
    {
        context.StartCoroutine(ReplayCoroutine());
    }

    public IEnumerator ReplayCoroutine()
    {
        if (commandHistory.Count == 0)
            yield break;
        
        float previousTime = 0f;

        foreach (var timedCommand in commandHistory)
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