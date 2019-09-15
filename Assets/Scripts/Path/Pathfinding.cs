using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class Pathfinding : SingletonMono<Pathfinding>
{
    public Grid GridReference;
    [HideInInspector] public Transform StartPosition;
    [HideInInspector] public Transform TargetPosition;
    private List<Node> _finalPath;
    private Coroutine moveCo;
    private readonly List<Vector3> positionCollection = new List<Vector3>();
    private readonly WaitForSeconds wait = new WaitForSeconds(.4f);

    public void Move()
    {
        moveCo = StartCoroutine(TranslateTargetCo());
    }

    public void StopMoveEnemy()
    {
        if (moveCo != null)
            StopCoroutine(moveCo);
    }

    private void ClearPath()
    {
        positionCollection.Clear();
    }

    private IEnumerator TranslateTargetCo()
    {
        foreach (var pos in positionCollection)
        {
            yield return wait;
            StartPosition.position = pos;
        }
    }

    public void FindPath()
    {
        ClearPath();
        if (GameManager.Instance != null)
            TargetPosition = GameManager.Instance.targetPoint;
        var startNode = GridReference.NodeFromWorldPoint(StartPosition.position);
        var targetNode = GridReference.NodeFromWorldPoint(TargetPosition.position);

        var openList = new List<Node>();
        var closedList = new HashSet<Node>();

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            var currentNode = openList[0];
            for (var i = 1; i < openList.Count; i++)
            {
                if (openList[i].FCost < currentNode.FCost ||
                    openList[i].FCost == currentNode.FCost && openList[i].CostToGoal < currentNode.CostToGoal)
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode)
            {
                GetFinalPath(startNode, targetNode);
            }

            var list = GridReference.GetNeighboringNodes(currentNode);
            foreach (var neighborNode in list)
            {
                if (!neighborNode.IsWall || closedList.Contains(neighborNode))
                {
                    continue;
                }

                var moveCost = currentNode.CostToNext + GetManhattenDistance(currentNode, neighborNode);

                if (moveCost < neighborNode.CostToNext || !openList.Contains(neighborNode))
                {
                    neighborNode.CostToNext = moveCost;
                    neighborNode.CostToGoal = GetManhattenDistance(neighborNode, targetNode);
                    neighborNode.ParentNode = currentNode;

                    if (!openList.Contains(neighborNode))
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }
    }

    private void GetFinalPath(Node startingNode, Node endNode)
    {
        _finalPath = new List<Node>();
        var currentNode = endNode;

        while (currentNode != startingNode)
        {
            _finalPath.Add(currentNode);
            currentNode = currentNode.ParentNode;
        }

        _finalPath.Reverse();

        foreach (var path in _finalPath)
        {
            var vp = path.VPosition;
            positionCollection.Add(vp);
        }

        GridReference.FinalPath = _finalPath;
    }

    private int GetManhattenDistance(Node aNodeA, Node aNodeB)
    {
        var x = Mathf.Abs(aNodeA.GridX - aNodeB.GridX);
        var y = Mathf.Abs(aNodeA.GridY - aNodeB.GridY);
        return x + y;
    }
}