using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MazeController mazeController;
    public IntVec mazeSize;
    public IntVec playerStartPoint;
    public IntVec enemyStartPoint;
    public IntVec endPoint;
    private MazeController _mazeController;
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        Begin();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Restart();
    }

    private void Begin()
    {
        _mazeController = Instantiate(mazeController);
        _mazeController.CreateMaze();
        _mazeController.ResetCharacter();
    }

    private void Restart()
    {
        if (Debug.isDebugBuild) Debug.LogWarning("restart");
        _mazeController.StopCreateMazeCo();
        Destroy(_mazeController.gameObject);
        Begin();
    }
}