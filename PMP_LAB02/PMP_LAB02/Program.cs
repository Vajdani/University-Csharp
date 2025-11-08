#region Util
static uint GetUnsignedNum(string text)
{
    while (true)
    {
        Console.Write(text);
        if (uint.TryParse(Console.ReadLine()!, out uint parsed))
        {
            return parsed;
        }
    }
}

static int GetPositiveNum(string text)
{
    while(true)
    {
        Console.Write(text);
        if (int.TryParse(Console.ReadLine()!, out int parsed) && parsed > 0)
        {
            return parsed;
        }
    }
}

static int GetPositiveNumInRange(string text, int min, int max)
{
    while (true)
    {
        Console.Write(text);
        if (int.TryParse(Console.ReadLine()!, out int parsed) && parsed > 0 && parsed >= min && parsed <= max)
        {
            return parsed;
        }
    }
}

static bool GetPassword(string text, string correctPassword)
{
    int tries = 0;
    int maxTries = 3;
    while (tries < maxTries)
    {
        Console.Write($"{text}(még {maxTries - tries} próbálokzása van): ");
        if (Console.ReadLine()! == correctPassword)
        {
            return true;
        }
        else
        {
            tries++;
        }
    }

    return false;
}

static void TaskPause()
{
    Console.ReadKey();
    Console.ResetColor();
    Console.Clear();
}
#endregion

//#region Task 1
//int count = GetPositiveNum("Adjon meg egy pozitív egész számot: ") + 1;
//for (int i = 0; i < count; i++)
//{
//    //Console.WriteLine(i);
//    if (i % 2 == 0)
//    {
//        Console.WriteLine(i);
//    }
//}
//#endregion

//TaskPause();

//#region Task 2
//Console.Write("Adjon meg egy jelszót: ");
//string password = Console.ReadLine()!;
//Console.Clear();
//if (GetPassword("Írja be mégegyszer a jelszavát", password))
//{
//    Console.WriteLine("Sikeres jelszó megerősítés.");
//}
//else
//{
//    Console.WriteLine("Sikertelen jelszó megerősítés.");
//}
//#endregion

//TaskPause();

//#region Task 3
//int num = GetPositiveNumInRange("Adjon meg egy számot 1 és 1000 között: ", 1, 1000);
//int generations = 0;
//while (true)
//{
//    if (num == Random.Shared.Next(1, 1001))
//    {
//        Console.WriteLine($"A gépnek {generations} próbálkozás kellett, hogy kitalálja a számot.");
//        break;
//    }
//    else
//    {
//        generations++;
//    }
//}
//#endregion

//TaskPause();

//#region Task 4
//int playerTries = 0;
//while (true)
//{
//    int pId = playerTries % 6 + 1;
//    Console.Write($"Játékos {pId} nyomjon entert!");
//    while (Console.ReadKey().Key != ConsoleKey.Enter) { }

//    int roll = Random.Shared.Next(1, 7);
//    Console.WriteLine($"\nJátékos {pId} dobása: {roll}");
//    if (roll == 6)
//    {
//        Console.WriteLine($"Játékos {pId} kezdi a játékot!");
//        break;
//    }
//    else
//    {
//        playerTries++;
//    }

//    Console.WriteLine();
//}
//#endregion

//TaskPause();

//#region Task 5
//int guesses = 0;
//int machineGuess = Random.Shared.Next(0, 101);
//Console.WriteLine("Gondoltam egy számra! Találja ki!");
//while (true)
//{
//    int playerGuess = GetPositiveNumInRange("Próbálkozásom: ", 0, 100);
//    if (playerGuess == machineGuess)
//    {
//        Console.WriteLine($"Ügyes volt! Kitalálta a számot! Összesen {guesses} próbálkozásba tellett!");
//        break;
//    }
//    else if (playerGuess < machineGuess)
//    {
//        Console.WriteLine("Nagyobb számra gondoltam!\n");
//    }
//    else
//    {
//        Console.WriteLine("Kisebb számra gondoltam!\n");
//    }

//    guesses++;
//}
//#endregion

//TaskPause();

//#region Task 6
//int n = GetPositiveNum("Adjon meg egy pozitív egész számot: ");
//int divisionCount = 0;
//for (int i = 2; i < n; i++)
//{
//    if (n % i == 0)
//    {
//        divisionCount++;
//    }
//}

//Console.WriteLine(n % 2 == 0 ? "Páros szám" : "Páratlan szám");
//Console.WriteLine($"Osztók száma(1-et és {n}-t kihagyva): {divisionCount}");
//Console.WriteLine(divisionCount <= 2 ? "Prím szám" : "Összetett szám");
//#endregion

//TaskPause();

//#region Task 7
//int factorialInput = GetPositiveNumInRange("Adjon meg egy pozitív egész számot: ", 1, int.MaxValue);
//int factorialValue = factorialInput;
//for (int i = factorialInput - 1; i > 0; i--)
//{
//    factorialValue *= i;
//}
//Console.WriteLine($"A megadott szám faktoriálisa: {factorialValue}");
//#endregion

//TaskPause();

//#region Task 8
//int gridSize = 9;
//for (int x = 0; x < gridSize + 1; x++)
//{
//    for (int y = 0; y < gridSize + 1; y++)
//    {
//        bool xborder = x == 0;
//        bool yborder = y == 0;
//        if (xborder || yborder)
//        {
//            Console.BackgroundColor = ConsoleColor.White;
//            Console.ForegroundColor = ConsoleColor.Black;
//        }
//        else
//        {
//            Console.ResetColor();
//        }

//        Console.Write(xborder ? y.ToString().PadRight(3) : (yborder ? x.ToString().PadRight(3) : (x * y).ToString().PadRight(3)));
//    }
//    Console.WriteLine();
//}
//#endregion

//TaskPause();

//#region Task 9
//int seconds = GetPositiveNum("Kérem adjon meg egy időtartamot másodpercekben: ");
//int minutes = Convert.ToInt32(Math.Floor((double)seconds / 60));
//Console.WriteLine($"Visszaszámlálás indult {minutes} percre és {seconds - minutes * 60} másodpercre.");
//do
//{
//    System.Threading.Thread.Sleep(1000);
//    seconds--;

//    Console.Clear();
//    minutes = Convert.ToInt32(Math.Floor((double)seconds / 60));
//    Console.WriteLine($"Még {minutes} perc és {seconds - minutes * 60} másodperc van hátra.");
//}
//while (seconds > 0);

//Console.BackgroundColor = ConsoleColor.Red;
//Console.Clear();
//Console.Beep();
//#endregion

//TaskPause();

#region Task 10
uint convertNum = GetUnsignedNum("Adjon meg egy pozitív egész számot: ");
string bits = "";
while(convertNum > 1)
{
    if (convertNum%2 == 0)
    {
        bits += "1";
    }
    else
    {
        bits += "0";
    }

    convertNum /= 2;
}

Console.WriteLine(bits);
#endregion






//https://main.elearning.uni-obuda.hu/pluginfile.php/572158/mod_resource/content/0/PmP_gyakorlati_jegyzet_02.pdf