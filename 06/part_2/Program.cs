

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
                break;
            }
        }

        return [guardX, guardY];
    }

    public static List<int[]> GetObstaclesCoords(string[] MapArray)
    {
        List<int[]> ObstaclesCoordinates = [];

        for (int y = 0; y < MapArray.Length; y++)
        {
            for (int x = 0; x < MapArray[0].Length; x++)
            {
                if (MapArray[y][x] == '#')
                {
                    ObstaclesCoordinates.Add([x, y]);
                }
            }
        }
        return ObstaclesCoordinates;
    }

    public static bool Compare(List<int[]> compare1, int[] compare2)
    {
        return compare1.Any(coords => coords.SequenceEqual(compare2));
    }

    public static bool ObstacleAhead(List<int[]> obstaclesCoords, int[] guardCoords, Direction direction, int[] size)
    {
        int currentGuardX = guardCoords[0];
        int currentGuardY = guardCoords[1];

        return (
            !GoingOffEdge(guardCoords, direction, size) && (
                direction == Direction.up && Compare(obstaclesCoords, [currentGuardX, currentGuardY - 1]) ||
                direction == Direction.right && Compare(obstaclesCoords, [currentGuardX + 1, currentGuardY]) ||
                direction == Direction.down && Compare(obstaclesCoords, [currentGuardX, currentGuardY + 1]) ||
                direction == Direction.left && Compare(obstaclesCoords, [currentGuardX - 1, currentGuardY])
            )
        );
    }

    public static bool GoingOffEdge(int[] guardCoords, Direction direction, int[] size)
    {
        int currentGuardX = guardCoords[0];
        int currentGuardY = guardCoords[1];

        int width = size[0];
        int height = size[1];

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

    public static int[] TakeStepForward(int[] guardCoords, Direction direction)
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

    public static void PrintListOfArrays(List<int[]> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine($"Array {i + 1}: {string.Join(", ", list[i])}");
        }
    }


    public static bool StuckInLoop(List<int[]> obstacles, int[] guardCoords, Direction direction, int[] size)
    {
        // each journal element = [x, y, dir]
        // PrintListOfArrays(obstacles);
        List<int[]> journal = [];

        while (true)
        {
            if (GoingOffEdge(guardCoords, direction, size))
            {
                return false;
            }
            else if (ObstacleAhead(obstacles, guardCoords, direction, size))
            {
                direction = UpdateDirection(direction);
            }
            else if (Compare(journal, [guardCoords[0], guardCoords[1], (int)direction]))
            {
                return true;
            }
            else
            {
                journal.Add([guardCoords[0], guardCoords[1], (int)direction]);
                guardCoords = TakeStepForward(guardCoords, direction);
            }
        }
    }

    public static List<int[]> JournalSteps(List<int[]> obstacles, int[] guardCoords, Direction direction, int[] size)
    {
        List<int[]> journal = [];

        while (true)
        {
            if (GoingOffEdge(guardCoords, direction, size))
            {
                journal.Add(guardCoords);
                break;
            }
            else if (ObstacleAhead(obstacles, guardCoords, direction, size))
            {
                direction = UpdateDirection(direction);
            }
            else
            {
                journal.Add(guardCoords);
                guardCoords = TakeStepForward(guardCoords, direction);
            }
        }

        return journal;
    }

    static void Main()
    {
        string fileContent = ReadFile("../input.txt");
        string[] MapArray = fileContent.Split("\n");
        MapArray = MapArray.Take(MapArray.Count() - 1).ToArray();

        int width = MapArray[0].Length;
        int height = MapArray.Length;

        int[] size = [width, height];

        int[] guardCoordsOriginal = GetGuardCoordinates(MapArray);
        List<int[]> obstacles = GetObstaclesCoords(MapArray);

        List<int[]> validObstacleCoords = [];

        Direction currentDir = Direction.up;

        var journal = JournalSteps(obstacles, guardCoordsOriginal, currentDir, size);
        // PrintListOfArrays(journal);
        var JournalArray = journal.ToArray();

        for (var j = 1; j < JournalArray.Length; j++)
        {
            int[] newHypotheticalObstacle = JournalArray[j];

            List<int[]> hypotheticalObstacles = [.. obstacles, newHypotheticalObstacle];

            bool closedLoop = StuckInLoop(hypotheticalObstacles, guardCoordsOriginal, Direction.up, size);

            if (closedLoop && !Compare(validObstacleCoords, newHypotheticalObstacle))
            {
                validObstacleCoords.Add(newHypotheticalObstacle);
                Console.WriteLine($"New coordinate found: ({newHypotheticalObstacle[0]}, {newHypotheticalObstacle[1]})");
            }
        }

        var newValidObstacleCoords = validObstacleCoords.ToArray();

        Console.WriteLine(newValidObstacleCoords.Length);
    }
}