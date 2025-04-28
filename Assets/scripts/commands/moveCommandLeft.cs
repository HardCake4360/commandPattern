using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCommandLeft : ICommand
{
    private PlayerController player;
    public moveCommandLeft(PlayerController player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.Move(Vector3.left);
    }
}
