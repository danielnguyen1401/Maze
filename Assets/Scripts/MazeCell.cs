using UnityEngine;

public class MazeCell : MonoBehaviour
{
    public IntVec coordinates;
    private readonly Edge[] edges = new Edge[MazeDirections.Count];

    public Edge GetEdge(Direction direction)
    {
        return edges[(int) direction];
    }

    public void SetEdge(Direction direction, Edge edge)
    {
        edges[(int) direction] = edge;
    }
}