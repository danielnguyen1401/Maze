using Maze;
using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "Levels", menuName = "LevelConfig", order = 1)]
    public class LevelConfig : ScriptableObject
    {
        public Level[] Levels;
    }

    [System.Serializable]
    public class Level
    {
        public string name;
        public IntVec mazeSize;
        public IntVec playerStartPoint;
        public IntVec enemyStartPoint;
        public IntVec endPoint;
        public Vector2 pathSize;
    }
}