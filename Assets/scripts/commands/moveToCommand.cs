using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToCommand : ICommand
{
    private PlayerController player;
    private Vector3 targetPos;
    public moveToCommand(PlayerController player, Vector3 targetPos)
    {
        this.player = player;
        this.targetPos = targetPos;
    }

    public void Execute()
    {
        player.MoveTo(targetPos);
    }
}
