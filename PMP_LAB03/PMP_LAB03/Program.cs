#region Task 12
//bool[,] maze = new bool[4, 10];
bool[,] maze = {
    { true, false, true, true, true, false, true, true, true, true },
    { false, false, true, true, false, false, true, false, true, true },
    { false, true, true, true, true, true, true, false, false, true },
    { false, false, true, false, false, false, false, true, true, true },
};

int maze_x = maze.GetLength(0);
int maze_y = maze.GetLength(1);

int target_x = maze_x - 1;
int target_y = maze_y - 1;

//int pos_x = 0;
//int pos_y = 0;
int pos_x = 1;
int pos_y = 2;

Dictionary<(int, int), bool> path;

void Init()
{
    //for (int x = 0; x < maze.GetLength(0); x++)
    //{
    //    for (int y = 0; y < maze.GetLength(1); y++)
    //    {
    //        maze[x, y] = x == target_x && y == target_y || Random.Shared.Next(1, 101) > 50;
    //    }
    //}

    //do
    //{
    //    pos_x = Random.Shared.Next(0, maze_x);
    //    pos_y = Random.Shared.Next(0, maze_y);
    //}
    //while (maze[pos_x, pos_y] == false || pos_x == target_x || pos_y == target_y);
    
    int pos_x = 1;
    int pos_y = 2;

    path = [];
    path.Add((pos_x, pos_y), true);
    path.Add((target_x, target_y), true);
}

void PrintBoard()
{
    for (int x = 0; x < maze_x; x++)
    {
        for (int y = 0; y < maze_y; y++)
        {
            if (path.ContainsKey((x, y)))
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.Write(maze[x, y] ? " T " : " F ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

bool CheckPosition(int new_pos_x, int new_pos_y)
{
    return
        new_pos_x >= 0 && new_pos_x < maze_x &&
        new_pos_y >= 0 && new_pos_y < maze_y &&
        (!path.ContainsKey((new_pos_x, new_pos_y)) || new_pos_x == target_x && new_pos_y == target_y) &&
        maze[new_pos_x, new_pos_y] == true;
}

bool TakeStep((int, int) new_pos)
{
    pos_x = new_pos.Item1;
    pos_y = new_pos.Item2;

    if (pos_x == target_x && pos_y == target_y)
    {
        Console.WriteLine("A program sikeresen talált útvonalat!");
        PrintBoard();

        return true;
    }
    else
    {
        path.Add((pos_x, pos_y), true);
        PrintBoard();
        
        return false;
    }
}

bool TraverseMaze()
{
    while (true)
    {
        List<(int, int)> possibleSteps = [];
        for (int x = 0; x < 2; x++)
        {
            if (CheckPosition(pos_x + x * 2 - 1, pos_y))
            {
                possibleSteps.Add((pos_x + x * 2 - 1, pos_y));
            }
        }

        for (int y = 0; y < 2; y++)
        {
            if (CheckPosition(pos_x, pos_y + y * 2 - 1))
            {
                possibleSteps.Add((pos_x, pos_y + y * 2 - 1));
            }
        }

        if (possibleSteps.Count == 0)
        {
            Console.WriteLine("Sajnos nem talált a program útvonalat.");
            return false;
        }
        else if (possibleSteps.Count == 1)
        {
            if (TakeStep(possibleSteps[0])) return true;
        }
        else
        {
            Dictionary<(int, int), bool> orig_path = new(path);
            for (int i = 0; i < possibleSteps.Count; i++)
            {
                TakeStep(possibleSteps[i]);

                if (TraverseMaze())
                {
                    return true;
                }
                else
                {
                    path = orig_path;
                }
            }

            //int min_dist = -1;
            //int id = -1;
            //for (int i = 0; i < possibleSteps.Count; i++)
            //{
            //    (int, int) pos = possibleSteps[i];
            //    int dist = Math.Abs(target_x - pos.Item1) + Math.Abs(target_y - pos.Item2);
            //    if (min_dist == -1 || dist < min_dist)
            //    {
            //        min_dist = dist;
            //        id = i;
            //    }
            //}

            //if (TakeStep(possibleSteps[id])) return true;
        }
    }
}

while (true)
{
    Init();
    
    Console.Clear();
    PrintBoard();

    TraverseMaze();

    if (Console.ReadKey().Key == ConsoleKey.Escape)
    {
        break;
    }
}

#endregion