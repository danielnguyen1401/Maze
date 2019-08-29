using Manager;
using UnityEngine;

namespace Maze
{
    public class MazeWall : Edge
    {
        public override void Initialize(MazeCell cell, MazeCell otherCell, Direction direction)
        {
            base.Initialize(cell, otherCell, direction);
            var tileSize = GameManager.Instance.tileSize;
            var localScale = transform.localScale;
            localScale = new Vector3(localScale.x * tileSize, localScale.y * tileSize, localScale.z);
            transform.localScale = localScale;
            var scale = 0.5f / tileSize;
            var intV = direction.ConvertIntVec();
            var x = (float) intV.x;
            var z = (float) intV.z;
            x *= scale;
            z *= scale;
            transform.localPosition = new Vector3(x, 0, z);
        }
    }
}