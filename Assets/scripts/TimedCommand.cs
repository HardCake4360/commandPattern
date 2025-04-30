using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedCommand
{
    public ICommand Command;
    public float Timestamp;
    public Vector3 PosStamp;

    public TimedCommand(ICommand command, float timestamp, Vector3 posStamp)
    {
        Command = command;
        Timestamp = timestamp;
        PosStamp = posStamp;
    }
}