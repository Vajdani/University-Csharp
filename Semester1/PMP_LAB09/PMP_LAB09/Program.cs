using PMP_LAB09;

string[] maps = Directory.GetFiles("maps/");
Console.WriteLine($"Maps:\n\t{String.Join("\n\t", maps)}");
string selectedMap;

while(true)
{
    Console.Write("Enter map number: ");
    if (int.TryParse(Console.ReadLine(), out int id) && id > 0 && id <= maps.Length)
    {
        selectedMap = maps[id - 1];
        break;
    }
}

Game game = new();
game.LoadMapFromPlainText(selectedMap);
game.Run();