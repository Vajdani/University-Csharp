Book b = new("The Hobbit - or There and Back Again", "J.R.R. Tolkien", 1937, 312);
Console.WriteLine(b.AllData());

Rectangle[] rects = [
    new(10, 10, ConsoleColor.Red    ),
    new(10, 20, ConsoleColor.Green  ),
    new(5,   5, ConsoleColor.Blue   ),
    new(20,  0, ConsoleColor.Magenta),
];
int offset_x = 0;
for (int i = 0; i < rects.Length; i++)
{
    if (rects[i].isValid())
    {
        rects[i].Draw(offset_x + 5 * i, 1);
        offset_x += rects[i].Width * 2 - 3;
    }
    else
    {
        Console.WriteLine($"Rect {i} is not valid!");
    }
}

Console.ReadKey();

int maxDistance = 20;
Runner r1 = new("ABCD", 0, 1);
Runner r2 = new("EFGH", 1, 2);

Console.Clear();
Console.WriteLine("Futóverseny! Nyomj meg valamit hogy elkezd!");
Console.ReadKey();
while (true)
{
    Console.Clear();
    r1.RefreshDistance(1);
    r2.RefreshDistance(1);
    r1.Show();
    r2.Show();

    if (r1.GetDistance() > maxDistance)
    {
        Console.Clear();
        Console.WriteLine($"{r1.Name} nyert!");
        break;
    }
    else if (r2.GetDistance() > maxDistance)
    {
        Console.Clear();
        Console.WriteLine($"{r2.Name} nyert!");
        break;
    }

    Thread.Sleep(1000);
}

Console.ReadKey();
Console.Clear();

Obfuscate obf = new(20);
string encoded = obf.Encode("Hi! I am your mom!");
Console.WriteLine(encoded);
Console.WriteLine(obf.Decode(encoded));



class Book {
    string Author;
    string Title;
    int PublishDate;
    int PageCount;

    public Book(string title, string author, int publishDate, int pageCount)
    {
        this.Author = author;
        this.Title = title;
        this.PublishDate = publishDate;
        this.PageCount = pageCount;
    }

    public string AllData()
    {
        return $"{Author}: {Title}, {PublishDate} ({PageCount} pages)"; 
    }
}

class Rectangle
{
    public int Width;
    public int Height;
    public ConsoleColor Colour;

    public Rectangle(int width, int height, ConsoleColor colour)
    {
        this.Width = width;
        this.Height = height;
        this.Colour = colour;
    }

    public int Area()
    {
        return Width * Height;
    }

    public bool isValid()
    {
        return Area() > 0;
    }

    public void Draw(int x, int y)
    {
        Console.ForegroundColor = Colour;
        for (int i = 0; i < Height; i++)
        {
            Console.SetCursorPosition(x, y + i);
            int width = (int)(Width * 1.5);
            if (i == 0 || i == Height - 1)
            {
                Console.WriteLine(new string('-',  width + 2));
            }
            else
            {
                Console.WriteLine($"|{new string(' ', width)}|");
            }
        }
        Console.ResetColor();
    }
}

class Runner
{
    public string Name;
    int Number;
    int Speed;
    int Distance;

    public Runner(string name, int number, int speed)
    {
        Name = name;
        Number = number;
        Speed = speed;
        Distance = 0;
    }

    public void RefreshDistance(int seconds)
    {
        Distance += Speed * seconds;
    }

    public void Show()
    {
        Console.SetCursorPosition(0, Number);
        Console.WriteLine($"{Name[0].ToString().PadLeft((int)(Distance))}");
    }

    public int GetDistance()
    {
        return Distance;
    }
}

class Obfuscate
{
    int CaesarShift;

    public Obfuscate(int caesarShift)
    {
        CaesarShift = caesarShift;
    }

    string TransformMessage(string message, int shift)
    {
        string output = "";
        for (int i = 0; i < message.Length; i++)
        {
            output += (char)((int)message[i] + shift);
        }

        return output;
    }

    public string Encode(string message)
    {
        return TransformMessage(message, CaesarShift);
    }

    public string Decode(string message)
    {
        return TransformMessage(message, -CaesarShift);
    }
}