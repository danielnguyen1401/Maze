using System;
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
            transform.localScale = new Vector3(transform.localScale.x * tileSize, transform.localScale.y * tileSize, transform.localScale.z);
            var scale = 0.5f / tileSize;
        
            // todo: format this class
        
            switch (direction)
            {
                case Direction.Bac:
                    transform.localPosition = new Vector3(0f, 0, scale);
                    break;
                case Direction.Nam:
                    transform.localPosition = new Vector3(0f, 0, -scale);
                    break;
                case Direction.Dong:
                    transform.localPosition = new Vector3(scale, 0, 0);
                    break;
                case Direction.Tay:
                    transform.localPosition = new Vector3(-scale, 0, 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

//        transform.localPosition = new Vector3(transform.localPosition.x * multi.x, 0, transform.localPosition.z * multi.z);
        }
    }
}