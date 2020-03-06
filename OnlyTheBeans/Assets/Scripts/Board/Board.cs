using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Board 
{
    public enum Direction
    {
        East,
        West,
        North,
        South,
        Up,
        Down
    }

    public static float GetBoardDistance(Vector3 space, Vector3 piece) {
        return Vector3.Distance(space, new Vector3(piece.x, space.y, piece.z));
    }

    public static Direction FlipDirection(Direction direction)
    {
        if (direction == Direction.East) return Direction.West;
        else if (direction == Direction.West) return Direction.East;
        else if (direction == Direction.North) return Direction.South;
        else if (direction == Direction.South) return Direction.North;
        else if (direction == Direction.Up) return Direction.Down;
        else return Direction.Up;
    }
    public static Direction GetDirection(Vector3 direction)
    {
        direction = direction.normalized;
        if (direction.x >= 0.5f) return Direction.East;
        else if (direction.x < -0.5f) return Direction.West;
        else if (direction.z > 0.5f) return Direction.North;
        else return Direction.South;
    }

}
