using UnityEngine;
using System.Collections;

public class Position
{
    public int x, y;

    public Position(int X, int Y)
    {
        this.x = X;
        this.y = Y;
    }

    public Position MoveSouth(int n)
    {
        return new Position(x, y + n);
    }
    public Position MoveNorth(int n)
    {
        return new Position(x, y - n);
    }
    public Position MoveWest(int n)
    {
        return new Position(x - n, y);
    }
    public Position MoveEast(int n)
    {
        return new Position(x + n, y);
    }
}
