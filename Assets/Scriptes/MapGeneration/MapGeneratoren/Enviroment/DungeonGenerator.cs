using UnityEngine;
using System.Collections;
using System;

public class DungeonGenerator : IMapGenerator
{
    public int MapWidth;
    public int MapHeight;
    public int[,] RandomMap;

    public int[,] Generate()
    {
        GenerateTowns(MapWidth, MapHeight);
        return RandomMap;
    }

    public void GenerateTowns(int width, int heigth)
    {
        Position CenterOfMap = new Position(MapWidth / 2, MapHeight / 2);
        Room CenterRoom = new Room(CenterOfMap, 5, 5, 9);
        FillElement(CenterRoom, 9);

        for (int i = 0; i < 25; i++)
        {
            int count = 0;
            while (count < 32)
            {
                count++;
                int ChooseElement = UnityEngine.Random.Range(0, 2);
                Position Wall = PickWall();

                if (Wall == null)
                    continue;

                Room newRoom;
                if (ChooseElement == 0)
                {
                    newRoom = NewRandomRoom(Wall, 10, 10, 9);
                }
                else
                {
                    newRoom = newRandomWay(Wall, 2, 10, 17);
                }
                Position roomPosition = Wall;
                switch (CheckDoorDirection(Wall))
                {
                    case Direction.West:
                        roomPosition = roomPosition.MoveWest(newRoom.width);
                        break;
                    case Direction.East:
                        roomPosition = roomPosition.MoveEast(1);
                        break;
                    case Direction.North:
                        roomPosition = roomPosition.MoveNorth(newRoom.height);
                        break;
                    case Direction.South:
                        roomPosition = roomPosition.MoveSouth(1);
                        break;
                    default:
                        break;
                }
                newRoom = newRoom.MoveTo(roomPosition);
                if (ScanArea(newRoom))
                {
                    FillElement(newRoom, newRoom.Number);
                    RandomMap[Wall.x, Wall.y] = 1 + 8 + 4 + 1024;
                    break;
                }
            }
        }
    }

    private Direction CheckDoorDirection(Position Wall)
    {
        int rightNumber = RandomMap[Wall.x + 1, Wall.y];
        int leftNumber = RandomMap[Wall.x - 1, Wall.y];
        int upNumber = RandomMap[Wall.x, Wall.y - 1];
        if (rightNumber == 9)
        {
            return Direction.West;
        }
        if (leftNumber == 9)
        {
            return Direction.East;
        }
        if (upNumber == 9)
        {
            return Direction.South;
        }
        else
        {
            return Direction.North;
        }
    }

    private Room newRandomWay(Position Wall, int Width, int Height, int tilenumber)
    {
        int width = UnityEngine.Random.Range(2, Width);
        int height = UnityEngine.Random.Range(2, Height);
        Room room = new Room(Wall, width, height, tilenumber);
        return room;
    }

    private Room NewRandomRoom(Position Wall, int width, int height, int tilenumber)
    {
        int Width = UnityEngine.Random.Range(4, width);
        int Height = UnityEngine.Random.Range(4, height);
        Room room = new Room(Wall, Width, Height, tilenumber);
        return room;
    }

    private Position PickWall()
    {
        for (int i = 0; i < 64; i++)
        {
            int posX = UnityEngine.Random.Range(1, RandomMap.GetLength(0) - 1);
            int posY = UnityEngine.Random.Range(1, RandomMap.GetLength(1) - 1);
            if (isRoomWall(posX, posY))
            {
                return new Position(posX, posY);
            }
        }
        return null;
    }

    private bool isRoomWall(int posX, int posY)
    {
        int chooseNumber = RandomMap[posX, posY];
        int rightNumber = RandomMap[posX + 1, posY];
        int leftNumber = RandomMap[posX - 1, posY];
        int upNumber = RandomMap[posX, posY - 1];
        int downNumber = RandomMap[posX, posY + 1];
        if ((chooseNumber & 1) > 0 && (leftNumber == 9 || rightNumber == 9 || upNumber == 9 || downNumber == 9))
        {
            return true;
        }
        return false;
    }

    private void FillElement(Room CenterRoom, int tilenumber)
    {
        if (ScanArea(CenterRoom))
        {
            for (int x = CenterRoom.position.x; x < CenterRoom.width + CenterRoom.position.x; x++)
            {
                for (int y = CenterRoom.position.y; y < CenterRoom.height + CenterRoom.position.y; y++)
                {
                    RandomMap[x, y] = tilenumber;
                }
            }
        }
    }

    private bool ScanArea(Room CenterRoom)
    {
        if (CenterRoom.position.x + CenterRoom.width >= RandomMap.GetLength(0) ||
            CenterRoom.position.y + CenterRoom.height >= RandomMap.GetLength(1))
        {
            return false;
        }
        if (CenterRoom.position.x <= 0 || CenterRoom.position.y <= 0)
        {
            return false;
        }
        for (int x = CenterRoom.position.x - 1; x < CenterRoom.position.x + CenterRoom.width + 1; x++)
        {
            for (int y = CenterRoom.position.y - 1; y < CenterRoom.position.y + CenterRoom.height + 1; y++)
            {
                if (x < 0 || y < 0 || x >= RandomMap.GetLength(0) || y >= RandomMap.GetLength(1))
                    continue;

                if (RandomMap[x, y] != 1)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void Setup(int width, int height, int[,] Randommap)
    {
        MapWidth = width;
        MapHeight = height;
        RandomMap = Randommap;
    }

    public void Modify()
    {
    }
}
