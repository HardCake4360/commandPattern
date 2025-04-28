using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedCommand
{
    public ICommand Command;
    public float Timestamp;

    public TimedCommand(ICommand command, float timestamp)
    {
        Command = command;
        Timestamp = timestamp;
    }
}