using Camera;
using Level;
using Maze;
using UnityEngine;
using UnityEngine.AI;

namespace Manager
{
    public class GameManager : SingletonMono<GameManager>
    {
        public MazeController mazeController;
        public int tileSize;

        private IntVec _mazeSize;
        private IntVec _playerStartPoint;
        private IntVec _enemyStartPoint;
        private IntVec _endPoint;


        public NavMeshSurface navMeshSurface;
        public LevelConfig levelConfig;
        [HideInInspector] public Transform targetPoint;
        private MazeController _mazeController;
        public bool GameStarted { get; set; }
        public bool GameEnded { get; set; }
        public int LevelId { get; set; }

        private void Awake()
        {
            LevelId = 0;
        }

        private void LoadLevels()
        {
            _mazeSize = levelConfig.Levels[LevelId].mazeSize;
            _playerStartPoint = levelConfig.Levels[LevelId].playerStartPoint;
            _enemyStartPoint = levelConfig.Levels[LevelId].enemyStartPoint;
            _endPoint = levelConfig.Levels[LevelId].endPoint;
            Debug.LogWarning("LoadLevels");
        }

        public IntVec GetMazeSize => _mazeSize;
        public IntVec GetPlayerStartPoint => _playerStartPoint;
        public IntVec GetEnemyStartPoint => _enemyStartPoint;
        public IntVec GetEndPoint => _endPoint;


        private void Start()
        {
            Begin();
        }

        private void Begin()
        {
            LoadLevels();
            Debug.LogWarning("LEVEL ID: " + LevelId);
            _mazeController = Instantiate(mazeController);
            _mazeController.CreateMaze();
//            _mazeController.ResetCharacter();
        }

        private void Restart()
        {
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
            Destroy(_mazeController.gameObject);
            GameUiManager.Instance.ShowWinLose(false);
            LevelId++;
            Begin();
        }
    }
}