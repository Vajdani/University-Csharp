//#region Task 1
//Console.Write("Írjon be egy mondatot: ");
//char[] input = Console.ReadLine()!.ToCharArray();
//int letterCount = 0;
//int numberCount = 0;
//int vowelCount  = 0;
//string vowels = "aeiouáéíóöőúüű";
//foreach (char c in input)
//{
//    if (c == ' ')
//    {
//        continue;
//    }

//    if (char.IsLetter(c))
//    {
//        letterCount++;
//        if (vowels.Contains(char.ToLower(c)))
//        {
//            vowelCount++;
//        }
//    }
//    else if (char.IsDigit(c))
//    {
//        numberCount++;
//    }
//}

//Console.WriteLine($"Betűk száma: {letterCount}");
//Console.WriteLine($"Számok száma: {numberCount}");
//Console.WriteLine($"Magánhangzók száma: {vowelCount}");
//#endregion

//#region Task 9 rossz kód
//string parenthesis = "[(])";
////string parenthesis = "[()][()()]()";

//Dictionary<char, char> pairing = new() {
//    { ')', '(' },
//    { ']', '[' },
//    { '}', '{' },
//};

//List<char> chars = [];

//bool failed = false;
//for (int i = 0; i < parenthesis.Length; i++)
//{
//    if(pairing.ContainsValue(parenthesis[i]))
//    {
//        chars.Add(parenthesis[i]);
//    }
//    else if(pairing.ContainsKey(parenthesis[i]))
//    {
//        if(chars[^1] != pairing[parenthesis[i]])
//        {
//            failed = true;
//            break;
//        }
//        else
//        {
//            chars.RemoveAt(chars.Count - 1);
//        }
//    }
//}

//if (failed)
//{
//    Console.WriteLine("Hibás sorozat!");
//}
//else
//{
//    Console.WriteLine("Hibátlan sorozat!");
//}
//#endregion

//#region Task 10
//char[,] workspace = new char[50, 20];
//int cursor_x = 0;
//int cursor_y = 0;
//while (true)
//{
//    Console.ResetColor();
//    Console.Clear();

//    for (int i = 0; i < workspace.GetLength(0); i++)
//    {
//        for (int j = 0; j < workspace.GetLength(1); j++)
//        {
//            if (i == cursor_x && j == cursor_y)
//            {
//                Console.BackgroundColor = ConsoleColor.White;
//                Console.ForegroundColor = ConsoleColor.Black;
//                Console.Write("o");
//                continue;
//            }
//            else if (workspace[i, j] == (char)0)
//            {
//                Console.BackgroundColor = ConsoleColor.Black;
//                Console.ForegroundColor = ConsoleColor.Black;
//                Console.Write("g");
//                continue;
//            }

//            Console.BackgroundColor = ConsoleColor.Black;
//            Console.ForegroundColor = ConsoleColor.White;
//            Console.Write(workspace[i, j]);
//        }
//        Console.WriteLine();
//    }

//    switch (Console.ReadKey().Key)
//    {
//        case ConsoleKey.Escape:
//            return;
//        case ConsoleKey.Enter:
//            if (cursor_x < workspace.GetLength(1) - 1)
//            {
//                cursor_x++;
//            }
//            break;
//        case ConsoleKey.RightArrow:
//            if (cursor_y == workspace.GetLength(0) - 1)
//            {
//                cursor_y = 0;
//                cursor_x++;
//            }
//            else if (cursor_x < workspace.GetLength(1) - 1)
//            {
//                cursor_y++;
//            }
//            break;
//        case ConsoleKey.LeftArrow:
//            if (cursor_y == 0 && cursor_x > 0)
//            {
//                cursor_y = workspace.GetLength(0) - 1;
//                cursor_x--;
//            }
//            else
//            {
//                cursor_y--;
//            }
//            break;
//        case ConsoleKey.UpArrow:
//            if (cursor_x > 0)
//            {
//                cursor_x--;
//            }
//            break;
//        case ConsoleKey.DownArrow:
//            if (cursor_x < workspace.GetLength(1) - 1)
//            {
//                cursor_x++;
//            }
//            break;
//    }

//    //Console.SetCursorPosition(cursor_x, cursor_y);
//}
//#endregion

#region Task 11
using System.Collections;

Console.Write("Írjon be egy mondatot: ");
char[] inputb64 = Console.ReadLine()!.ToCharArray();
string converted = "";
char[] characters = [
    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
    'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
    'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',
    'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
    '+', '/'
];

//for (int i = 0; i < inputb64.Length; i += 3)
//{
//    //Console.WriteLine($"{i} {inputb64[i]}");
//    byte[] bytes = new byte[Math.Clamp(inputb64.Length - i, 0, 3)];
//    for (int b = 0; b < bytes.Length; b++)
//    {
//        bytes[b] = (byte)inputb64[i + b];
//    }

//    BitArray bitArray = new(bytes);
//    for (int j = 0; j < bitArray.Length/6; j++)
//    {
//        int offset = j * 6;
//        int index = 0;
//        for (int k = 0; k < 6; k++)
//        {
//            if (bitArray[offset + k])
//            {
//                index += (int)Math.Pow(2, k);
//            }
//        }

//        //Console.WriteLine($"char {index} {characters[index]}");
//        converted += characters[index];
//    }
//}

for (int i = 0; i < inputb64.Length; i += 3)
{
    BitArray bitArray = new(System.Text.Encoding.UTF8.GetBytes(inputb64[i..(i + 3)]));
    Console.WriteLine($"Bits: {bitArray.Count}");
    for (int j = 0; j < 4; j++)
    {
        string bits = "";
        int offset = j * 6;
        int index = 0;
        for (int k = 0; k < 6; k++)
        {
            //Console.Write($"{bitArray[offset + k]}({offset + k}) ");
            if (bitArray[offset + k])
            {
                bits += "1";
                index += (int)Math.Pow(2, k);
            }
            else
            {
                bits += "0";
            }
        }
        Console.WriteLine(bits);

        //Console.WriteLine($"Char index: {index} {characters[index]}");
        //Console.WriteLine($"char {index} {characters[index]}");
        converted += characters[index];
    }
}

Console.WriteLine(converted);
#endregion