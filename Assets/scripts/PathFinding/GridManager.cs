using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public int width = 10;
    public int height = 10;
    public float gridSize = 1f;

    private Dictionary<Vector2Int, Node> grid = new(); //그리드 생성

    void CreateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int pos = new(x, y);
                Node node = new(pos);

                Vector3 worldPos = GridToWorld(pos);
                Collider[] colliders = Physics.OverlapSphere(worldPos, gridSize * 0.4f);
                foreach (var col in colliders)
                {
                    if (col.gameObject.CompareTag("Obstacle"))  // 태그로 구분
                    {
                        node.isWalkable = false;
                        break;
                    }
                }

                grid[pos] = node;
            }
        }
    }
    
    public void InitGrid()
    {
        foreach (var pair in grid)
        {
            Node node = pair.Value;
            node.gCost = int.MaxValue;
            node.hCost = 0;
            node.parent = null;
        }
    }

    private void Awake()
    {
        Instance = this;
        CreateGrid();
    }

    public Node GetNode(Vector2Int pos)
    {
        grid.TryGetValue(pos, out var node);//노드를 찾고 받아옴
        return node;
    }

    public List<Node> GetNeighbors(Node node)//인접 노드를 리스트로 반환
    {
        List<Node> neighbors = new();
        Vector2Int[] directions ={
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right,
        };

        foreach (var dir in directions)
        {
            Vector2Int neighborPos = node.gridPosition + dir;
            if (grid.ContainsKey(neighborPos))
                neighbors.Add(grid[neighborPos]);
        }

        return neighbors;
    }

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x / gridSize);
        int y = Mathf.FloorToInt(worldPos.z / gridSize);
        return new Vector2Int(x, y);
    }

    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x + 0.5f, 1, gridPos.y + 0.5f) * gridSize;
    }

    private void OnDrawGizmos()
    {
        if (grid == null)
            return;

        foreach (var pair in grid)
        {
            Node node = pair.Value;
            Vector3 worldPos = GridToWorld(node.gridPosition);

            Gizmos.color = node.isWalkable ? Color.white : Color.red;
            Gizmos.DrawWireCube(worldPos, Vector3.one * gridSize * 0.9f);
        }
    }
}
