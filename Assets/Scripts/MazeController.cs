using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        newCell.name = "cell-" + coordinates.x + "," + coordinates.z;
        newCell.transform.localPosition = new Vector3(coordinates.x - mazeSize.x * 0.5f + 0.5f, 0f, coordinates.z - mazeSize.z * 0.5f + 0.5f);
        return newCell;
    }

    private void FirstGenerate(List<MazeCell> cells)
    {
        cells.Add(CreateCell(RandomCoordinates));
    }

    private void NextGenerate(List<MazeCell> cells)
    {
        int currentIndex = cells.Count - 1;
        MazeCell currentCell = cells[currentIndex];
        Direction direction = MazeDirections.RandomValue;
        IntVec coordinates = currentCell.coordinates + direction.ToIntVector2();
        if (ContainsCoordinates(coordinates))
        {
            MazeCell neighbor = GetCell(coordinates);
            if (neighbor == null)
            {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                cells.Add(neighbor);
            }
            else
            {
                CreateWall(currentCell, neighbor, direction);
                cells.RemoveAt(currentIndex);
            }
        }
        else
        {
            CreateWall(currentCell, null, direction);
            cells.RemoveAt(currentIndex);
        }
    }

    private void CreatePassage(MazeCell cell, MazeCell otherCell, Direction direction)
    {
        var passage = Instantiate(passagePref);
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePref);
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }

    private void CreateWall(MazeCell cell, MazeCell otherCell, Direction direction)
    {
        var wall = Instantiate(wallPref);
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null)
        {
            wall = Instantiate(wallPref);
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

    private IntVec RandomCoordinates => new IntVec(Random.Range(0, mazeSize.x), Random.Range(0, mazeSize.z));

    private bool ContainsCoordinates(IntVec coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < mazeSize.x && coordinate.z >= 0 && coordinate.z < mazeSize.z;
    }

    private MazeCell GetCell(IntVec coordinate)
    {
        return _cells[coordinate.x, coordinate.z];
    }
}