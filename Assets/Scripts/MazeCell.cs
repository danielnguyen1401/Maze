using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeCell : MonoBehaviour
{
    public IntVec coordinates;
    private int _initEdgeCount;
    private readonly Edge[] _edges = new Edge[MazeDirections.Count];

    public bool IsGenerateCompleted => _initEdgeCount == MazeDirections.Count;

    public Direction RandomInitDirection
    {
        get
        {
            var directionCount = MazeDirections.Count;
            var noCall = Random.Range(0, directionCount - _initEdgeCount);
            for (var i = 0; i < directionCount; i++)
            {
                if (_edges[i] == null)
                {
                    if (noCall == 0)
                    {
                        return (Direction) i;
                    }
                    noCall -= 1;
                }
            }

            throw new InvalidOperationException();
        }
    }

    public Edge GetEdge(Direction direction)
    {
        return _edges[(int) direction];
    }

    public void SetEdge(Direction direction, Edge edge)
    {
        _edges[(int) direction] = edge;
        _initEdgeCount += 1;
    }
}