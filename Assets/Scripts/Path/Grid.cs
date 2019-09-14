using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask WallMask;
    public Vector2 GridWorldSize; //A vector2 to store the width and height of the graph in world units.
    public float NodeRadius; //This stores how big each square on the graph will be
    public float DistanceBetweenNodes; //The distance that the squares will spawn from eachother.

    public Node[,] NodeArray; //The array of nodes that the A Star algorithm uses.
    public List<Node> FinalPath; //The completed path that the red line will be drawn along

    float _nodeDiameter;
    int _gridSizeX, _gridSizeY;


    private void Start()
    {
        _nodeDiameter = NodeRadius * 2; //Double the radius to get diameter
        _gridSizeX = Mathf.RoundToInt(GridWorldSize.x / _nodeDiameter);
        _gridSizeY = Mathf.RoundToInt(GridWorldSize.y / _nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        NodeArray = new Node[_gridSizeX, _gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 -
                             Vector3.forward * GridWorldSize.y / 2;
        for (var x = 0; x < _gridSizeX; x++)
        {
            for (var y = 0; y < _gridSizeY; y++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * _nodeDiameter + NodeRadius) +
                                     Vector3.forward * (y * _nodeDiameter + NodeRadius);
                bool wall = !Physics.CheckSphere(worldPoint, NodeRadius, WallMask);

                NodeArray[x, y] = new Node(wall, worldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighboringNodes(Node neighbor)
    {
        List<Node> neighborList = new List<Node>(); //Make a new list of all available neighbors.

        var checkX = neighbor.GridX + 1;
        var checkY = neighbor.GridY;
        if (checkX >= 0 && checkX < _gridSizeX) //If the XPosition is in range of the array
        {
            if (checkY >= 0 && checkY < _gridSizeY) //If the YPosition is in range of the array
            {
                neighborList.Add(NodeArray[checkX, checkY]); //Add the grid to the available neighbors list
            }
        }

        //Check the Left side of the current node.
        checkX = neighbor.GridX - 1;
        checkY = neighbor.GridY;
        if (checkX >= 0 && checkX < _gridSizeX) //If the XPosition is in range of the array
        {
            if (checkY >= 0 && checkY < _gridSizeY) //If the YPosition is in range of the array
            {
                neighborList.Add(NodeArray[checkX, checkY]); //Add the grid to the available neighbors list
            }
        }

        //Check the Top side of the current node.
        checkX = neighbor.GridX;
        checkY = neighbor.GridY + 1;
        if (checkX >= 0 && checkX < _gridSizeX) //If the XPosition is in range of the array
        {
            if (checkY >= 0 && checkY < _gridSizeY) //If the YPosition is in range of the array
            {
                neighborList.Add(NodeArray[checkX, checkY]); //Add the grid to the available neighbors list
            }
        }

        //Check the Bottom side of the current node.
        checkX = neighbor.GridX;
        checkY = neighbor.GridY - 1;
        if (checkX >= 0 && checkX < _gridSizeX) //If the XPosition is in range of the array
        {
            if (checkY >= 0 && checkY < _gridSizeY) //If the YPosition is in range of the array
            {
                neighborList.Add(NodeArray[checkX, checkY]); //Add the grid to the available neighbors list
            }
        }

        return neighborList; //Return the neighbors list.
    }

    //Gets the closest node to the given world position.
    public Node NodeFromWorldPoint(Vector3 worldPos)
    {
        float ixPos = ((worldPos.x + GridWorldSize.x / 2) / GridWorldSize.x);
        float iyPos = ((worldPos.z + GridWorldSize.y / 2) / GridWorldSize.y);

        ixPos = Mathf.Clamp01(ixPos);
        iyPos = Mathf.Clamp01(iyPos);

        int ix = Mathf.RoundToInt((_gridSizeX - 1) * ixPos);
        int iy = Mathf.RoundToInt((_gridSizeY - 1) * iyPos);

        return NodeArray[ix, iy];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, 1, GridWorldSize.y));
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
                Gizmos.DrawCube(n.VPosition, Vector3.one * (_nodeDiameter - DistanceBetweenNodes));
            }
        }
    }

}