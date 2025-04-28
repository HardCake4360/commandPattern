using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCommandForward : ICommand
{
    private PlayerController player;
    public moveCommandForward(PlayerController player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.Move(Vector3.forward);
    }
}
