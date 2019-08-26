using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
// Growing Tree algorithm
// #1. Let C be a list of cells, initially empty. Add one cell to C, at random.
// #2. Choose a cell from C, and carve a passage to any unvisited neighbor of that cell, adding that neighbor to C as well.
//     If there are no unvisited neighbors, remove the cell from C.
// #3. Repeat #2 until C is empty.
// http://weblog.jamisbuck.org/2011/1/27/maze-generation-growing-tree-algorithm
// </summary>
public class MazeController : MonoBehaviour
{
    public MazeCell cellPrefab;
    public MazePassage passagePref;
    public MazeWall wallPref;
    private MazeCell[,] _cells;
    private readonly WaitForEndOfFrame _wait = new WaitForEndOfFrame();
    private Coroutine _mazeCoroutine;
    public IntVec mazeSize;
    private IntVec _coordinates;

    private void Start()
    {
    }

    public void CreateMaze()
    {
        _mazeCoroutine = StartCoroutine(GenerateCo());
    }

    public void StopCreateMazeCo()
    {
        if (_mazeCoroutine == null) return;
        StopCoroutine(_mazeCoroutine);
    }

    private IEnumerator GenerateCo()
    {
        _cells = new MazeCell[mazeSize.x, mazeSize.z];
        List<MazeCell> activeCells = new List<MazeCell>();
        FirstGenerate(activeCells);
        while (activeCells.Count > 0)
        {
            yield return _wait;
            NextGenerate(activeCells);
        }
    }

    private MazeCell CreateCell(IntVec coordinates)
    {
        var newCell = Instantiate(cellPrefab, transform);
        _cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "cell - " + coordinates.x + "," + coordinates.z;
        newCell.transform.localPosition = new Vector3(coordinates.x - mazeSize.x * 0.5f + 0.5f, 0f, coordinates.z - mazeSize.z * 0.5f + 0.5f);
        return newCell;
    }

    private void FirstGenerate(List<MazeCell> cells)
    {
        cells.Add(CreateCell(CoordinatesBeRandom));
    }

    private void NextGenerate(List<MazeCell> cells)
    {
        int currentIndex = cells.Count - 1;
        MazeCell currentCell = cells[currentIndex];
        if (currentCell.IsGenerateCompleted)
        {
            cells.RemoveAt(currentIndex);
            return;
        }

        var direction = currentCell.RandomInitDirection;
        IntVec coordinates = currentCell.coordinates + direction.ToIntVector2();
        if (ContainsCoordinate(coordinates))
        {
            MazeCell neighbor = GetCell(coordinates);
            if (neighbor == null)
            {
                neighbor = CreateCell(coordinates);
                MakePassage(currentCell, neighbor, direction);
                cells.Add(neighbor);
            }
            else
            {
                MakeWall(currentCell, neighbor, direction);
            }
        }
        else
        {
            MakeWall(currentCell, null, direction);
        }
    }

    private void MakePassage(MazeCell cell, MazeCell otherCell, Direction direction)
    {
        var passage = Instantiate(passagePref);
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePref);
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }

    private void MakeWall(MazeCell cell, MazeCell otherCell, Direction direction)
    {
        var wall = Instantiate(wallPref);
        wall.Initialize(cell, otherCell, direction);
        if (otherCell == null) return;
        wall = Instantiate(wallPref);
        wall.Initialize(otherCell, cell, direction.GetOpposite());
    }

    private IntVec CoordinatesBeRandom => new IntVec(Random.Range(0, mazeSize.x), Random.Range(0, mazeSize.z));

    private bool ContainsCoordinate(IntVec coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < mazeSize.x && coordinate.z >= 0 && coordinate.z < mazeSize.z;
    }

    private MazeCell GetCell(IntVec coordinate)
    {
        return _cells[coordinate.x, coordinate.z];
    }
}