using UnityEngine;
using System.Collections;

public class Room
{
    public int width, height;
    public Position position;
    public int Number;

    public Room(Position pos, int width, int height, int number)
    {
        this.Number = number;
        this.position = pos;
        this.width = width;
        this.height = height;
    }
    public Room MoveTo(Position pos)
    {
        return new Room(pos, width, height, Number);
    }
}
