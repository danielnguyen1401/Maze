using UnityEngine;

public class Edge : MonoBehaviour
{
    public MazeCell cell, otherCell;
	
    public Direction direction;
    
    public void Initialize (MazeCell cell, MazeCell otherCell, Direction direction) {
        this.cell = cell;
        this.otherCell = otherCell;
        this.direction = direction;
        cell.SetEdge(direction, this);
        transform.parent = cell.transform;
        transform.localPosition = Vector3.zero;
    }
}
