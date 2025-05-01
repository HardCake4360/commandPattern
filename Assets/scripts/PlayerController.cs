using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public void Move(Vector3 dir)
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }
    public void MoveTo(Vector3 targetPos)
    {
        //transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        Vector2Int targetGrid = GridManager.Instance.WorldToGrid(targetPos);
        Vector2Int currentGrid = GridManager.Instance.WorldToGrid(gameObject.transform.position);
        List<Node> path = ASPathfinding.FindPath(currentGrid, targetGrid);

        if (path != null)
            StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Node> path)
    {
        foreach (Node node in path)
        {
            Vector3 targetPos = GridManager.Instance.GridToWorld(node.gridPosition);
            while (Vector3.Distance(transform.position, targetPos) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 3f);
                yield return null;
            }
        }
    }

}
