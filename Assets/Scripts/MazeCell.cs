using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeCell : MonoBehaviour
{
    public IntVec coordinates;
    private int _initEdgeCount;
    private readonly Edge[] edges = new Edge[MazeDirections.Count];

    public bool IsGenerateCompleted => _initEdgeCount == MazeDirections.Count;

    public Direction RandomInitDirection
    {
        get
        {
            var directionCount = MazeDirections.Count;
            var skips = Random.Range(0, directionCount - _initEdgeCount);
            for (var i = 0; i < directionCount; i++)
            {
                if (edges[i] == null)
                {
                    if (skips == 0)
                    {
                        return (Direction) i;
                    }
                    skips -= 1;
                }
            }

            throw new InvalidOperationException();
        }
    }

    public Edge GetEdge(Direction direction)
    {
        return edges[(int) direction];
    }

    public void SetEdge(Direction direction, Edge edge)
    {
        edges[(int) direction] = edge;
        _initEdgeCount += 1;
    }
}