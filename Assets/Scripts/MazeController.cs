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
    [SerializeField] private GameObject playerPref;
    [SerializeField] private GameObject enemyPref;
    [SerializeField] private GameObject endPref;
    private MazeCell[,] _cells;
    private readonly WaitForEndOfFrame _waitAFrame = new WaitForEndOfFrame();
    private readonly WaitForSeconds _wait = new WaitForSeconds(2f);
    private Coroutine _mazeCoroutine;
    private IntVec _size;
    private IntVec _coordinates;
    private readonly Vector3 _offset = new Vector3(0, .5f, 0);
    private GameObject _playerGo;
    private GameObject _enemyGo;
    private GameObject _endObj;

    private void Awake()
    {
        _size = GameManager.Instance.mazeSize;
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
        _cells = new MazeCell[_size.x, _size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
        FirstGenerate(activeCells);
        while (activeCells.Count > 0)
        {
            yield return _waitAFrame;
            NextGenerate(activeCells);
        }

        yield return _waitAFrame;
        CreateCharacter();
        // bake the background here
    }

    public void ResetCharacter()
    {
        var playerP = GameManager.Instance.playerStartPoint;
        var enemyP = GameManager.Instance.enemyStartPoint;
        var endP = GameManager.Instance.endPoint;
        if (_playerGo != null) _playerGo.transform.position = _cells[playerP.x, playerP.z].transform.localPosition + _offset;
        if (_enemyGo != null) _enemyGo.transform.position = _cells[enemyP.x, enemyP.z].transform.localPosition + _offset;
        if (_endObj != null) _endObj.transform.position = _cells[endP.x, endP.z].transform.localPosition + new Vector3(0, .2f, 0);
    }

    private void CreateCharacter()
    {
        _playerGo = Instantiate(playerPref);
        _enemyGo = Instantiate(enemyPref);
        _endObj = Instantiate(endPref);
        ResetCharacter();
    }

    private MazeCell CreateCell(IntVec coordinates)
    {
        var newCell = Instantiate(cellPrefab, transform);
        _cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "cell - " + coordinates.x + "," + coordinates.z;
        newCell.transform.localPosition = new Vector3(coordinates.x - _size.x * 0.5f + 0.5f, 0f, coordinates.z - _size.z * 0.5f + 0.5f);

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
        if (cell.coordinates.x == GameManager.Instance.endPoint.x && cell.coordinates.z == GameManager.Instance.endPoint.z)
        {
            wall.gameObject.SetActive(false);
        }

        if (otherCell == null) return;
        wall = Instantiate(wallPref);
        wall.Initialize(otherCell, cell, direction.GetOpposite());
    }

    private IntVec RandomCoordinates => new IntVec(Random.Range(0, _size.x), Random.Range(0, _size.z));

    private bool ContainsCoordinate(IntVec coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < _size.x && coordinate.z >= 0 && coordinate.z < _size.z;
    }

    private MazeCell GetCell(IntVec coordinate)
    {
        return _cells[coordinate.x, coordinate.z];
    }

//    private bool IsPlayerStartPoint(IntVec cellPosition)
//    {
//        var pStartPos = GameManager.Instance.playerStartPoint;
//        var result = cellPosition.x == pStartPos.x && cellPosition.z == pStartPos.z;
//        return result;
//    }
//
//    private bool IsEnemyStartPoint(IntVec cellPosition)
//    {
//        var eStartPos = GameManager.Instance.enemyStartPoint;
//        var result = cellPosition.x == eStartPos.x && cellPosition.z == eStartPos.z;
//        return result;
//    }
//
//    private bool IsEndPoint(IntVec cellPosition)
//    {
//        var endPos = GameManager.Instance.endPoint;
//        var result = cellPosition.x == endPos.x && cellPosition.z == endPos.z;
//        return result;
//    }
}