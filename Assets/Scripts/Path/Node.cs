using UnityEngine;

public class Node
{
    public readonly int GridX;
    public readonly int GridY;
    public readonly bool IsWall;
    public Vector3 VPosition;
    public Node ParentNode;
    public int CostToNext;
    public int CostToGoal;

    public int FCost => CostToNext + CostToGoal;

    public Node(bool isWall, Vector3 vPos, int gridX, int gridY) 
    {
        IsWall = isWall;
        VPosition = vPos;
        GridX = gridX;
        GridY = gridY;
    }
}