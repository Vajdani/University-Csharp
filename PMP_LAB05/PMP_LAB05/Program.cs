//#region Task 1
//StreamReader reader = new("../../../data/colouredText.txt");
//while(!reader.EndOfStream)
//{
//    string line = reader.ReadLine()!;
//    Console.ForegroundColor = Enum.Parse<ConsoleColor>(line[..line.IndexOf('#')], true);
//    Console.WriteLine(line.Split('#')[1]);
//}
//reader.Close();
//#endregion

//Console.ReadKey();
//Console.ResetColor();
//Console.Clear();

//#region Task 2
//StreamWriter writer = new("../../../data/lotto.txt");
//int i = 0;
//while (true)
//{
//    int[] nums = new int[5];
//    for (int j = 0; j < 5; j++)
//    {
//        int num;
//        do
//        {
//            num = Random.Shared.Next(1, 100);
//        }
//        while (nums.Contains(num));

//        nums[j] = num;
//    }

//    string line = $"On {DateTime.Today.AddDays(i * 7).ToShortDateString()} numbers were: {String.Join(' ', nums)}";
//    Console.WriteLine(line);
//    writer.WriteLine(line);

//    Console.Write("Another week? [y/n] ");
//    if (Console.ReadLine() == "n")
//    {
//        break;
//    }

//    i++;
//}
//writer.Close();
//#endregion

//Console.ReadKey();
//Console.ResetColor();
//Console.Clear();

//#region Task 3
//reader = new("../../../data/ant.txt");
//string[] split = reader.ReadLine()!.Split(' ');
//int x = int.Parse(split[0]);
//int y = int.Parse(split[1]);
//int dir = int.Parse(split[2]);
//while (!reader.EndOfStream)
//{
//    string line = reader.ReadLine()!;
//    if (line.StartsWith("go"))
//    {
//        int go = int.Parse(line[3..]);
//        switch (dir)
//        {
//            case 0:
//                x += go;
//                break;
//            case 90:
//                y += go;
//                break;
//            case 180:
//                x -= go;
//                break;
//            case 270:
//                y -= go;
//                break;
//            default:
//                Console.WriteLine("Hangya nem szabályos irányba néz!");
//                break;
//        }
//    }
//    else if (line.StartsWith("left"))
//    {
//        dir += int.Parse(line[5..]);
//        if (dir > 360)
//        {
//            dir -= 360;
//        }
//    }
//    else if (line.StartsWith("right"))
//    {
//        dir -= int.Parse(line[5..]);
//        if (dir < 0)
//        {
//            dir += 360;
//        }
//    }
//}
//reader.Close();

//Console.WriteLine($"Hangya végleges pozíciója: x: {x} y: {y} irány: {dir}");
//#endregion

//Console.ReadKey();
//Console.ResetColor();
//Console.Clear();

#region Task 4
//reader = new("../../../data/NHANES_1999-2018");
Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

StreamReader reader = new("../../../data/NHANES_1999-2018.csv");
List<int>    SEQN     = [];
List<string> SURVEY   = [];
List<float>  RIAGENDR = [];
List<float>  RIDAGEYR = [];
List<float>  BMXBMI   = [];
List<float>  LBDGLUSI = [];

Dictionary<string, List<float>> surveys = []; 

reader.ReadLine();
while(!reader.EndOfStream)
{
    string[] entry = reader.ReadLine()!.Split(',');
    SEQN    .Add(  int.Parse(entry[0]));
    SURVEY  .Add(            entry[1] );
    RIAGENDR.Add(float.Parse(entry[2]));
    RIDAGEYR.Add(float.Parse(entry[3]));
    BMXBMI  .Add(float.Parse(entry[4]));
    LBDGLUSI.Add(float.Parse(entry[5]));

    surveys.TryAdd(entry[1], [0, 0, 0, 0, 0, 0]);
}
reader.Close();

const int BLOOD_SUGAR_TOTAL_INDEX = 0;
const int BLOOD_SUGAR_HIGH_INDEX = 1;
const int WOMEN_COUNT_INDEX = 2;
const int WOMEN_BMI_INDEX = 3;
const int MEN_COUNT_INDEX = 4;
const int MEN_BMI_INDEX = 5;

for (int j = 0; j < RIAGENDR.Count; j++)
{
    if (RIAGENDR[j] == 2)
    {
        surveys[SURVEY[j]][WOMEN_COUNT_INDEX]++;
        surveys[SURVEY[j]][WOMEN_BMI_INDEX] += BMXBMI[j];
    }
    else
    {
        surveys[SURVEY[j]][MEN_COUNT_INDEX]++;
        surveys[SURVEY[j]][MEN_BMI_INDEX] += BMXBMI[j];
    }

    surveys[SURVEY[j]][BLOOD_SUGAR_TOTAL_INDEX]++;
    if (LBDGLUSI[j] > 5.6)
    {
        surveys[SURVEY[j]][BLOOD_SUGAR_HIGH_INDEX]++;
    }
}

Console.WriteLine($"1. Átlagos testtömeg nemek szerint, felmérésenként:");
foreach (var item in surveys)
{
    Console.WriteLine($"{item.Key}: Nők: {item.Value[WOMEN_BMI_INDEX] / item.Value[WOMEN_COUNT_INDEX]:0.00} Férfiak: {item.Value[MEN_BMI_INDEX] / item.Value[MEN_COUNT_INDEX]:0.00}");
}

Console.WriteLine("2. feladat: Azon alanyok százaléka akiknek 5.6-nál magasabb volt a vércukorszintje, felmérésenként");
foreach (var item in surveys)
{
    Console.WriteLine($"{item.Key}: {item.Value[BLOOD_SUGAR_HIGH_INDEX] / item.Value[BLOOD_SUGAR_TOTAL_INDEX] * 100:0.00}%");
}

int maxBMI = 0;
float avgOverweightAge = 0;
float overWeightCount = 0;
for (int i = 1; i < BMXBMI.Count; i++)
{
    if (BMXBMI[i] > BMXBMI[maxBMI])
    {
        maxBMI = i;
    }

    if (BMXBMI[i] > 30)
    {
        avgOverweightAge += RIDAGEYR[i];
        overWeightCount++;
    }
}

Console.WriteLine($"3. feladat: Legnagyobb BMI-vel rendelkező alany cukorszintje: {LBDGLUSI[maxBMI]}");
Console.WriteLine($"4. feladat: A túlsúlyos alanyok átlagéletkora: {avgOverweightAge / overWeightCount:0.00}");
#endregion