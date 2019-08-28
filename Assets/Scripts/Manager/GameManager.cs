using Maze;
using UnityEngine;
using UnityEngine.AI;

namespace Manager
{
    public class GameManager : SingletonMono<GameManager>
    {
        public MazeController mazeController;
        public IntVec mazeSize;
        public int tileSize;
        public IntVec playerStartPoint;
        public IntVec enemyStartPoint;
        public IntVec endPoint;
        public NavMeshSurface navMeshSurface;
        [HideInInspector] public Transform targetPoint;
        private MazeController _mazeController;
        public bool GameStarted { get; set; }

        private void Awake()
        {
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

        public void BakeNavMesh()
        {
            navMeshSurface.BuildNavMesh();
        }

        public void StartGame()
        {
            GameStarted = true;
        }

        public void NextGame()
        {
//        GameStarted = true;
        }
    }
}