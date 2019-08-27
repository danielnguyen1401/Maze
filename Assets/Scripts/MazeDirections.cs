using UnityEngine;

public enum Direction
{
    Bac,
    Dong,
    Nam,
    Tay
}

public static class MazeDirections
{
    public const int Count = 4;
//    public static Direction RandomValue => (Direction) Random.Range(0, Count);

    private static readonly IntVec[] Vectors =
    {
        new IntVec(0, 1),
        new IntVec(1, 0),
        new IntVec(0, -1),
        new IntVec(-1, 0)
    };

    private static readonly Direction[] Opposites =
    {
        Direction.Nam,
        Direction.Tay,
        Direction.Bac,
        Direction.Dong
    };

    private static readonly Quaternion[] Rotations =
    {
        Quaternion.identity,
        Quaternion.Euler(0f, 90f, 0f),
        Quaternion.Euler(0f, 180f, 0f),
        Quaternion.Euler(0f, 270f, 0f)
    };

    public static IntVec ConvertIntVec(this Direction direction)
    {
        return Vectors[(int) direction];
    }

    public static Direction GetOpposite(this Direction direction)
    {
        return Opposites[(int) direction];
    }

    public static Quaternion GetRotate(this Direction direction)
    {
        return Rotations[(int) direction];
    }
}