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
    public static Direction RandomValue => (Direction) Random.Range(0, Count);

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

    public static IntVec ToIntVector2(this Direction direction)
    {
        return Vectors[(int) direction];
    }

    public static Direction GetOpposite(this Direction direction)
    {
        return Opposites[(int) direction];
    }
}