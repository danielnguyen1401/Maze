using System.Collections.Generic;
using Manager;
using UnityEngine;

public class Grid : SingletonMono<Grid>
{
    public LayerMask WallMask;
    private Vector2 _gridWorldSize;
    public float NodeRadius;
    private float _distanceBetweenNodes = 0.1f;

    public Node[,] NodeArray;
    public List<Node> FinalPath;

    float _nodeDiameter;
    int _gridSizeX, _gridSizeY;


    public void CreateGrid()
    {
        _gridWorldSize = GameManager.Instance.PathSize;
        _nodeDiameter = NodeRadius * 2;
        _gridSizeX = Mathf.RoundToInt(_gridWorldSize.x / _nodeDiameter);
        _gridSizeY = Mathf.RoundToInt(_gridWorldSize.y / _nodeDiameter);
        DrawGrid();
    }

    private void DrawGrid()
    {
        NodeArray = new Node[_gridSizeX, _gridSizeY];
        var bottomLeft = transform.position - Vector3.right * _gridWorldSize.x / 2 -
                             Vector3.forward * _gridWorldSize.y / 2;
        for (var x = 0; x < _gridSizeX; x++)
        {
            for (var y = 0; y < _gridSizeY; y++)
            {
                var worldPoint = bottomLeft + Vector3.right * (x * _nodeDiameter + NodeRadius) +
                                     Vector3.forward * (y * _nodeDiameter + NodeRadius);
                var wall = !Physics.CheckSphere(worldPoint, NodeRadius, WallMask);

                NodeArray[x, y] = new Node(wall, worldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighboringNodes(Node neighbor)
    {
        var neighborList = new List<Node>();

        var checkX = neighbor.GridX + 1;
        var checkY = neighbor.GridY;
        if (checkX >= 0 && checkX < _gridSizeX)
        {
            if (checkY >= 0 && checkY < _gridSizeY)
            {
                neighborList.Add(NodeArray[checkX, checkY]);
            }
        }

        //Check the Left 
        checkX = neighbor.GridX - 1;
        checkY = neighbor.GridY;
        if (checkX >= 0 && checkX < _gridSizeX)
        {
            if (checkY >= 0 && checkY < _gridSizeY)
            {
                neighborList.Add(NodeArray[checkX, checkY]);
            }
        }

        checkX = neighbor.GridX;
        checkY = neighbor.GridY + 1;
        if (checkX >= 0 && checkX < _gridSizeX)
        {
            if (checkY >= 0 && checkY < _gridSizeY)
            {
                neighborList.Add(NodeArray[checkX, checkY]);
            }
        }

        //Check the Bottom
        checkX = neighbor.GridX;
        checkY = neighbor.GridY - 1;
        if (checkX >= 0 && checkX < _gridSizeX)
        {
            if (checkY >= 0 && checkY < _gridSizeY)
            {
                neighborList.Add(NodeArray[checkX, checkY]);
            }
        }

        return neighborList;
    }

    public Node NodeFromWorldPoint(Vector3 worldPos)
    {
        var ixPos = ((worldPos.x + _gridWorldSize.x / 2) / _gridWorldSize.x);
        var iyPos = ((worldPos.z + _gridWorldSize.y / 2) / _gridWorldSize.y);

        ixPos = Mathf.Clamp01(ixPos);
        iyPos = Mathf.Clamp01(iyPos);

        var ix = Mathf.RoundToInt((_gridSizeX - 1) * ixPos);
        var iy = Mathf.RoundToInt((_gridSizeY - 1) * iyPos);

        return NodeArray[ix, iy];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(_gridWorldSize.x, 1, _gridWorldSize.y));
        if (NodeArray != null)
        {
            foreach (Node n in NodeArray)
            {
                Gizmos.color = n.IsWall ? Color.white : Color.yellow;

                if (FinalPath != null)
                {
                    if (FinalPath.Contains(n))
                    {
                        Gizmos.color = new Color(0.33f, 0.58f, 1f);
                    }
                }

                Gizmos.DrawCube(n.VPosition, Vector3.one * (_nodeDiameter - _distanceBetweenNodes));
            }
        }
    }
}