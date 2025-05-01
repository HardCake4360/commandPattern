using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector2Int gridPosition;
    public bool isWalkable = true;
    public Node parent;

    public int gCost;//��������->���� ���
    public int hCost;//���� ���->��ǥ���� ������� (�޸���ƽ)
    public int fCost => gCost + hCost;

    public Node(Vector2Int pos)
    {
        gridPosition = pos;
    }
}
