int[,] tomb = new int[10, 20];
for (int i = 0; i < tomb.GetLength(0); i++)
{
    for (int j = 0; j < tomb.GetLength(1); j++)
    {
        tomb[i, j] = Random.Shared.Next(0, 101);
    }
}

for (int i = 0; i < tomb.GetLength(0); i++)
{
    for (int j = 0; j < tomb.GetLength(1); j++)
    {
        Console.Write($"{tomb[i,j],3} ");
    }

    Console.WriteLine();
}