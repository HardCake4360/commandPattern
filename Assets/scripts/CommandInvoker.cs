using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandInvoker : MonoBehaviour
{
    private List<TimedCommand> commandHistory = new List<TimedCommand>();
    private float startTime;

    public int maxHistorySize = 100;
    public Slider HistoryBar;

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

        if(commandHistory.Count >= maxHistorySize)
            commandHistory.RemoveAt(0);
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

    private void Update()
    {
        HistoryBar.value = commandHistory.Count / maxHistorySize;
        Debug.Log(commandHistory.Count);
    }
}