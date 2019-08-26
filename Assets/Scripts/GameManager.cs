using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MazeController mazeController;
    private MazeController _mazeController;

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
    }

    private void Restart()
    {
        if(Debug.isDebugBuild) Debug.LogWarning("restart");
        _mazeController.StopCreateMazeCo();
        Destroy(_mazeController.gameObject);
        Begin();
    }
}