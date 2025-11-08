//#region Task 1
//Console.Write("Adja meg a játékosok számát: ");
//Player[] RandomPlayers = new Player[int.Parse(Console.ReadLine()!)];
//for (int i = 0; i < RandomPlayers.Length; i++)
//{
//    RandomPlayers[i] = new Player($"Játékos {i + 1}", (Position)Random.Shared.Next(0, 4));
//}

//Team team = new();
//while (!team.isFull)
//{
//    Console.Clear();

//    Console.WriteLine($"Szabad játékosok:");
//    foreach (Player player in RandomPlayers)
//    {
//        if (!team.isIncluded(player))
//        {
//            Console.WriteLine($"\t{player}");
//        }
//    }

//    if (team.NumberOfPlayers > 0)
//    {
//        Console.WriteLine("A csapat játékosai:");
//        for (int i = 0; i < team.NumberOfPlayers; i++)
//        {
//            Console.WriteLine($"\t{team.players[i]}");
//        }
//    }
//    else
//    {
//        Console.WriteLine("A csapat jelenleg üres.");
//    }

//    Console.Write("Melyik játékost adja hozzá a csapathoz? ");
//    string name = Console.ReadLine()!;
//    foreach (Player player in RandomPlayers)
//    {
//        if (player.Name == name)
//        {
//            team.Include(player);
//            break;
//        }
//    }

//    Console.ReadKey();
//}

//Console.WriteLine("Sikeresen létrehozta a csapatát!");
//for (int i = 0; i < 5; i++)
//{
//    Console.WriteLine($"\t{team.players[i]}");
//}
//#endregion

#region Task 2
using System.Text;

Game game = new(25, 10);
game.Run();
#endregion



enum Position
{
    Goalkeeper,
    Forward,
    Winger,
    Defender
}

class Player
{
    public string Name;
    public Position Pos;

    public Player(string name, Position pos)
    {
        Name = name;
        Pos = pos;
    }

    public override string ToString()
    {
        return $"{Name} ({Pos})";
    }
}

class Team
{
    public Player[] players = new Player[5];
    public int NumberOfPlayers = 0;
    public bool isFull => NumberOfPlayers == 5;

    static Dictionary<Position, int> maxPlayerCounts = new() {
        { Position.Goalkeeper, 1 },
        { Position.Forward, 1 },
        { Position.Winger, 2 },
        { Position.Defender, 1 },
    };

    public bool isIncluded(Player player)
    {
        return players.Contains(player);
    }

    public bool isAvailable(Player player)
    {
        int count = 0;
        for (int i = 0; i < NumberOfPlayers; i++)
        {
            if (players[i].Pos == player.Pos)
            {
                count++;
            }
        }

        return count < maxPlayerCounts[player.Pos];
    }

    public void Include(Player player)
    {
        if (NumberOfPlayers == 5)
        {
            Console.WriteLine("Ez a csapat már tele van!");
            return;
        }

        if (isIncluded(player))
        {
            Console.WriteLine($"{player.Name} már szerepel a csapatban!");
            return;
        }

        if (!isAvailable(player))
        {
            Console.WriteLine($"{player.Pos} féle játékosból csak {maxPlayerCounts[player.Pos]} lehet!");
            return;
        }

        players[NumberOfPlayers] = player;
        NumberOfPlayers++;

        Console.WriteLine("Sikeres hozzáadás.");
    }
}



class Field
{
    int Size;
    int[,] Map;
    public int TargetX => Map.GetLength(0) - 1;
    public int TargetY => Map.GetLength(1) - 1;
    
    public Field(int size)
    {
        Size = size;
        Map = new int[size, size];
    }

    public bool AllowedPosition(int x, int y)
    {
        return x >= 0 && x <= TargetX && y >= 0 && y <= TargetY;
    }

    public void Show()
    {
        int width = TargetX * 2 + 5;
        StringBuilder topBottom = new(width);
        topBottom.Insert(0, "-", width);

        //int quarter  = (int)Math.Ceiling(width * 0.25);
        //int half     = (int)Math.Ceiling(width * 0.50);
        //int quarter2 = (int)Math.Ceiling(width * 0.75);
        //topBottom.Insert(quarter,  quarter);
        //topBottom.Insert(half,     half);
        //topBottom.Insert(quarter2, quarter2);

        topBottom[(int)Math.Ceiling(width * 0.25)] = 'W';
        topBottom[(int)Math.Ceiling(width * 0.50)] = 'W';
        topBottom[(int)Math.Ceiling(width * 0.75)] = 'W';

        string middle = $"|{new string(' ', TargetX * 2 + 3)}|\n";
        Console.WriteLine(topBottom);
        Console.Write(new StringBuilder(middle.Length * TargetY + 1).Insert(0, middle, TargetY + 1).ToString());
        Console.WriteLine(topBottom);
    }
}

class Buffalo
{
    int x;
    int y;
    public bool active = true;

    public int X => x;
    public int Y => y;

    public void Move(Field field)
    {
        if (!active)
        {
            return;
        }

        while (true)
        {
            int x_next = x;
            int y_next = y;
            switch (Random.Shared.Next(0, 3))
            {
                case 0:
                    x_next++;
                    break;
                case 1:
                    y_next++;
                    break;
                case 2:
                    x_next++;
                    y_next++;
                    break;
            }

            if (field.AllowedPosition(x_next, y_next))
            {
                x = x_next;
                y = y_next;
                break;
            }
        }
    }

    public void Deactivate()
    {
        active = false;
    }

    public void Show()
    {
        Console.SetCursorPosition(x * 2 + 2, y + 1);
        Console.ForegroundColor = active ? ConsoleColor.Green : ConsoleColor.Red;
        Console.Write("B");
        Console.ResetColor();
    }
}

class Game
{
    Field field;
    Buffalo[] buffalos;

    public bool isOver { get; private set; }

    public Game(int fieldSize, int buffaloCount)
    {
        field = new(fieldSize);

        buffalos = new Buffalo[buffaloCount];
        for (int i = 0; i < buffaloCount; i++)
        {
            buffalos[i] = new();
        }
    }

    public void VisualizeElements()
    {
        Console.Clear();
        field.Show();

        foreach (Buffalo buffalo in buffalos)
        {
            buffalo.Move(field);
            buffalo.Show();
        }
    }

    public void Shoot()
    {
        Console.SetCursorPosition(0, field.TargetY + 3);   
        Console.Write("Adja meg a lövés x koordinátáját: ");
        int x = int.Parse(Console.ReadLine()!);
        Console.Write("Adja meg a lövés y koordinátáját: ");
        int y = int.Parse(Console.ReadLine()!);

        foreach (Buffalo buffalo in buffalos)
        {
            if (buffalo.X == x && buffalo.Y == y)
            {
                buffalo.Deactivate();
            }
        }
    }

    public void Run()
    {
        while (!isOver)
        {
            VisualizeElements();
            Shoot();
            //Thread.Sleep(500);

            int aliveBuffalos = 0;
            foreach (Buffalo buffalo in buffalos)
            {
                if (!buffalo.active)
                {
                    continue;
                }

                aliveBuffalos++;
                if (buffalo.X == field.TargetX && buffalo.Y == field.TargetY)
                {
                    isOver = true;
                }
            }

            if (aliveBuffalos == 0)
            {
                isOver = true;
            }
        }

        Console.SetCursorPosition(0, field.TargetY + 3);
        Console.WriteLine("Vége a játéknak!");
    }
}