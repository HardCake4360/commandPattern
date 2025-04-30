using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerController player;
    public CommandInvoker invoker;

    private void Start()
    {
        invoker.StartRecording();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            invoker.ExecuteCommand(new moveCommandForward(player));
        if (Input.GetKey(KeyCode.S))
            invoker.ExecuteCommand(new moveCommandBackward(player));
        if (Input.GetKey(KeyCode.A))
            invoker.ExecuteCommand(new moveCommandLeft(player));
        if (Input.GetKey(KeyCode.D))
            invoker.ExecuteCommand(new moveCommandRight(player));

        if (Input.GetMouseButton(0)) // 좌클릭
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                invoker.ExecuteCommand(new moveToCommand(player, hit.point));
            }
        }

        if (Input.GetMouseButtonDown(1)) // 우클릭
        {
            invoker.ReplayCommands(this);
            player.transform.position = invoker.commandHistory[0].PosStamp;
        }
    }
}