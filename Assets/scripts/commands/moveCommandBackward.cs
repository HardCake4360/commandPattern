using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCommandBackward : ICommand
{
    private PlayerController player;
    public moveCommandBackward(PlayerController player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.Move(-Vector3.forward);
    }
}
