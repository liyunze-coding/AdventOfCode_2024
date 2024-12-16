

using System.Text;

class AdventOfCodeDay6
{
    public enum Direction
    {
        up = 0,
        right,
        down,
        left
    }

    public static string ReadFile(string filename)
    {
        string readText = File.ReadAllText(filename);
        readText = readText.Replace("\t", "");
        readText = readText.Replace("\r", "");
        return readText;
    }

    public static int[] GetGuardCoordinates(string[] MapArray)
    {
        int guardX = 0;
        int guardY = 0;

        // find coordinate of ^
        for (int guardYIndex = 0; guardYIndex < MapArray.Length; guardYIndex++)
        {
            int guardXIndex = MapArray[guardYIndex].IndexOf("^");

            if (guardXIndex > -1)
            {
                guardX = guardXIndex;
                guardY = guardYIndex;
            }
        }

        return [guardX, guardY];
    }

    public static string[] UpdateCoordChar(string[] MapArray, char character, int x, int y)
    {
        StringBuilder currentRow = new StringBuilder(MapArray[y]);
        currentRow[x] = character;
        MapArray[y] = currentRow.ToString();

        return MapArray;
    }

    public static string[] UpdateMap(string[] MapArray, int[] guardCoords, Direction direction)
    {
        int currentGuardX = guardCoords[0];
        int currentGuardY = guardCoords[1];
        int width = MapArray[0].Length;
        int height = MapArray.Length;

        // If going off the edge, don't update the guard's position and only mark X
        if (direction == Direction.up)
        {
            if (!GoingOffEdge(MapArray, guardCoords, direction))
            {
                MapArray = UpdateCoordChar(MapArray, '^', currentGuardX, currentGuardY - 1);
            }

            MapArray = UpdateCoordChar(MapArray, 'X', currentGuardX, currentGuardY);
        }
        else if (direction == Direction.right)
        {
            if (!GoingOffEdge(MapArray, guardCoords, direction))
            {
                MapArray = UpdateCoordChar(MapArray, '>', currentGuardX + 1, currentGuardY);
            }

            MapArray = UpdateCoordChar(MapArray, 'X', currentGuardX, currentGuardY);
        }
        else if (direction == Direction.down)
        {
            if (!GoingOffEdge(MapArray, guardCoords, direction))
            {
                MapArray = UpdateCoordChar(MapArray, 'v', currentGuardX, currentGuardY + 1);
            }

            MapArray = UpdateCoordChar(MapArray, 'X', currentGuardX, currentGuardY);
        }
        else if (direction == Direction.left)
        {
            if (!GoingOffEdge(MapArray, guardCoords, direction))
            {
                MapArray = UpdateCoordChar(MapArray, 'v', currentGuardX - 1, currentGuardY);
            }

            MapArray = UpdateCoordChar(MapArray, 'X', currentGuardX, currentGuardY);
        }

        return MapArray;
    }

    public static bool ObstacleAhead(string[] MapArray, int[] guardCoords, Direction direction)
    {
        int currentGuardX = guardCoords[0];
        int currentGuardY = guardCoords[1];

        return (
            !GoingOffEdge(MapArray, guardCoords, direction) && (
                direction == Direction.up && MapArray[currentGuardY - 1][currentGuardX] == '#' ||
                direction == Direction.right && MapArray[currentGuardY][currentGuardX + 1] == '#' ||
                direction == Direction.down && MapArray[currentGuardY + 1][currentGuardX] == '#' ||
                direction == Direction.left && MapArray[currentGuardY][currentGuardX - 1] == '#'
            )
        );
    }

    public static bool GoingOffEdge(string[] MapArray, int[] guardCoords, Direction direction)
    {
        int currentGuardX = guardCoords[0];
        int currentGuardY = guardCoords[1];

        int width = MapArray[0].Length;
        int height = MapArray.Length;

        return (
            direction == Direction.up && currentGuardY == 0 ||
            direction == Direction.right && currentGuardX == width - 1 ||
            direction == Direction.down && currentGuardY == height - 1 ||
            direction == Direction.left && currentGuardX == 0
        );
    }

    public static Direction UpdateDirection(Direction direction)
    {
        if (direction == Direction.left)
        {
            return Direction.up;
        }
        else
        {
            return direction + 1;
        }
    }

    public static int[] UpdateGuardCoords(int[] guardCoords, Direction direction)
    {
        int GuardX = guardCoords[0];
        int GuardY = guardCoords[1];
        switch (direction)
        {
            case Direction.up:
                GuardY--;
                break;
            case Direction.left:
                GuardX--;
                break;
            case Direction.down:
                GuardY++;
                break;
            case Direction.right:
                GuardX++;
                break;
            default:
                break;
        }

        return [GuardX, GuardY];
    }

    public static void PrintMap(string[] MapArray)
    {
        for (int y = 0; y < MapArray.Length; y++)
        {
            Console.WriteLine(MapArray[y]);
        }
    }

    public static int CountX(string[] MapArray)
    {
        int count = 0;
        int width = MapArray[0].Length;
        int height = MapArray.Length;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (MapArray[y][x] == 'X')
                {
                    count++;
                }
            }
        }

        return count;
    }

    static void Main()
    {
        string fileContent = ReadFile("../input.txt");
        string[] MapArray = fileContent.Split("\n");
        MapArray = MapArray.Take(MapArray.Count() - 1).ToArray();


        int[] guardCoords = GetGuardCoordinates(MapArray);

        Direction currentDir = Direction.up;

        bool guardInMap = true;

        while (guardInMap)
        {
            // PrintMap(MapArray);
            if (ObstacleAhead(MapArray, guardCoords, currentDir))
            {
                currentDir = UpdateDirection(currentDir);
            }
            else if (GoingOffEdge(MapArray, guardCoords, currentDir))
            {
                MapArray = UpdateMap(MapArray, guardCoords, currentDir);
                guardInMap = false;
            }
            else
            {
                MapArray = UpdateMap(MapArray, guardCoords, currentDir);

                guardCoords = UpdateGuardCoords(guardCoords, currentDir);
            }
        }

        // PrintMap(MapArray);
        Console.WriteLine(CountX(MapArray));
    }
}