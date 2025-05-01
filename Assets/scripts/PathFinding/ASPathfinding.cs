using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASPathfinding : MonoBehaviour
{
    static int GetHuristic(Node a,Node b) //남은 거리의 추정(각 좌표의 차를 더한 값의 절댓값)
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

        //시작, 도착노드 설정
        Node startNode = grid.GetNode(startPos);
        Node endNode = grid.GetNode(endPos);

        List<Node> openSet = new() { startNode }; //방문하지 않은 후보
        HashSet<Node> closedSet = new(); // 방문한 노드

        startNode.gCost = 0;
        startNode.hCost = GetHuristic(startNode, endNode);

        while (openSet.Count > 0) // 방문하지 않은 후보 노드가 남아있으면 계속
        {
            Node current = openSet[0];
            foreach(var node in openSet)
            {
                if (node.fCost < current.fCost
               || (node.fCost == current.fCost && node.hCost < current.hCost))
                    //비용이 적은 노드를 current에 넣음
                    //비용이 같다면 휴리스틱을 비교해 더 낮은 쪽을 선택
                    current = node;
            }

            //현재 노드가 도착지점이라면 역추적하여 Path생성
            if (current == endNode) return ReconstructPath(endNode);

            //방문한 노드 제거 후 이동
            openSet.Remove(current);
            closedSet.Add(current);

            //현재 노드의 이웃 순회
            foreach(var neighbor in grid.GetNeighbors(current))
            {
                //지나갈 수 없거나 이미검사했으면 스킵
                if (!neighbor.isWalkable || closedSet.Contains(neighbor)) continue;

                int tentativeG = current.gCost + 1; //임시 gCost계산

                if(tentativeG < neighbor.gCost //임시 비용이 기존보다 짧으면 교체 
                    || !openSet.Contains(neighbor)) //아직 방문하지 않았다면 교체
                {
                    neighbor.parent = current; // 부모 설정(역추적용)
                    
                    //비용 재설정
                    neighbor.gCost = tentativeG;
                    neighbor.hCost = GetHuristic(neighbor, endNode);

                    //이웃 노드를 방문하지 않은 노드에 추가해서 다음 루프때 검사함
                    if (!openSet.Contains(neighbor)) 
                        openSet.Add(neighbor);
                }

            }

        }
        return null; //경로 없음
    }
}
