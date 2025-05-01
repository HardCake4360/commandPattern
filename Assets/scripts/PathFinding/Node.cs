using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector2Int gridPosition;
    public bool isWalkable = true;
    public Node parent;

    public int gCost;//시작지점->현재 노드
    public int hCost;//현재 노드->목표지점 추정비용 (휴리스틱)
    public int fCost => gCost + hCost;

    public Node(Vector2Int pos)
    {
        gridPosition = pos;
    }
}
