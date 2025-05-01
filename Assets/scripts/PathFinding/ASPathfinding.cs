using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASPathfinding : MonoBehaviour
{
    static int GetHuristic(Node a,Node b) //���� �Ÿ��� ����(�� ��ǥ�� ���� ���� ���� ����)
    {
        return Mathf.Abs(a.gridPosition.x - b.gridPosition.x)
            + Mathf.Abs(a.gridPosition.y - b.gridPosition.y);
    }

    static List<Node> ReconstructPath(Node endNode)
    {
        List<Node> path = new();
        Node current = endNode;
        while(current != null)
        {
            path.Add(current);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }

    public static List<Node> FindPath(Vector2Int startPos, Vector2Int endPos)
    {
        GridManager grid = GridManager.Instance;

        //����, ������� ����
        Node startNode = grid.GetNode(startPos);
        Node endNode = grid.GetNode(endPos);

        List<Node> openSet = new() { startNode }; //�湮���� ���� �ĺ�
        HashSet<Node> closedSet = new(); // �湮�� ���

        startNode.gCost = 0;
        startNode.hCost = GetHuristic(startNode, endNode);

        while (openSet.Count > 0) // �湮���� ���� �ĺ� ��尡 ���������� ���
        {
            Node current = openSet[0];
            foreach(var node in openSet)
            {
                if (node.fCost < current.fCost
               || (node.fCost == current.fCost && node.hCost < current.hCost))
                    //����� ���� ��带 current�� ����
                    //����� ���ٸ� �޸���ƽ�� ���� �� ���� ���� ����
                    current = node;
            }

            //���� ��尡 ���������̶�� �������Ͽ� Path����
            if (current == endNode) return ReconstructPath(endNode);

            //�湮�� ��� ���� �� �̵�
            openSet.Remove(current);
            closedSet.Add(current);

            //���� ����� �̿� ��ȸ
            foreach(var neighbor in grid.GetNeighbors(current))
            {
                //������ �� ���ų� �̹̰˻������� ��ŵ
                if (!neighbor.isWalkable || closedSet.Contains(neighbor)) continue;

                int tentativeG = current.gCost + 1; //�ӽ� gCost���

                if(tentativeG < neighbor.gCost //�ӽ� ����� �������� ª���� ��ü 
                    || !openSet.Contains(neighbor)) //���� �湮���� �ʾҴٸ� ��ü
                {
                    neighbor.parent = current; // �θ� ����(��������)
                    
                    //��� �缳��
                    neighbor.gCost = tentativeG;
                    neighbor.hCost = GetHuristic(neighbor, endNode);

                    //�̿� ��带 �湮���� ���� ��忡 �߰��ؼ� ���� ������ �˻���
                    if (!openSet.Contains(neighbor)) 
                        openSet.Add(neighbor);
                }

            }

        }
        return null; //��� ����
    }
}
