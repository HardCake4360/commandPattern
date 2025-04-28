using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCommandRight : ICommand
{
    private PlayerController player;
    public moveCommandRight(PlayerController player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.Move(Vector3.right);
    }
}
