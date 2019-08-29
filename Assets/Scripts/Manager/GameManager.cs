using Enemy;
using Level;
using Maze;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Manager
{
    public class GameManager : SingletonMono<GameManager>
    {
        public MazeController mazeController;
        [HideInInspector] public int tileSize;
        public GameObject winLoseFx;
        public NavMeshSurface navMeshSurface;
        public LevelConfig levelConfig;
        
        private IntVec _mazeSize;
        private IntVec _playerStartPoint;
        private IntVec _enemyStartPoint;
        private IntVec _endPoint;
        [HideInInspector] public Transform targetPoint;
        private MazeController _mazeController;
        public bool GameStarted { get; set; }
        public bool GameEnded { get; set; }
        private int _levelId;
        public int LevelId => _levelId;

        private void LoadLevels()
        {
            _mazeSize = levelConfig.Levels[_levelId].mazeSize;
            _playerStartPoint = levelConfig.Levels[_levelId].playerStartPoint;
            _enemyStartPoint = levelConfig.Levels[_levelId].enemyStartPoint;
            _endPoint = levelConfig.Levels[_levelId].endPoint;
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
            _mazeController = Instantiate(mazeController);
            _mazeController.CreateMaze();
        }

        public void BakeNavMesh()
        {
            navMeshSurface.BuildNavMesh();
        }

        public void StartGame()
        {
            GameStarted = true;
            EnableAgentCo();
        }

        private void EnableAgentCo()
        {
            PlayerController.Instance.EnableAgent();
            EnemyController.Instance.EnableAgent();
        }

        public void NextGame()
        {
            Destroy(_mazeController.gameObject);
            StopAllCoroutines();
            GameUiManager.Instance.ShowWinLose(false);
            GameStarted = false;
            GameEnded = false;
            MakeLevelCircle();
            CameraManager.Instance.Cache();
            Begin();
        }

        private void MakeLevelCircle()
        {
            _levelId++;
            if (_levelId >= levelConfig.Levels.Length)
                _levelId = 0;
            GameUiManager.Instance.nextLevel.Invoke();
        }

        public void PlayWinLoseEffect(Vector3 position)
        {
            var fx = Instantiate(winLoseFx);
            fx.transform.position = position;
        }
    }
}