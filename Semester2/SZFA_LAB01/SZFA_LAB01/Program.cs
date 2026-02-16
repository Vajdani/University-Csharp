using SZFA_LAB01;

if (args.Length > 0)
{
    Console.WriteLine($"Folder arg detected: {args[0]}");

    Cage[] cages2 = Cage.MakeZooFromFolder(args[0]);
    Console.WriteLine($"Cages read from folder: {cages2.Length}");
    foreach (Cage cage_ in cages2)
    {
        cage_.PrintContents();
    }
}
else
{
    Console.WriteLine("Code generated cage:");

    Cage cage1 = new(5);
    cage1.Add(new Animal("Állat 1", false, 10, Species.Panda));
    cage1.Add(new Animal("Állat 2", true,  10, Species.Rabbit));
    cage1.Add(new Animal("Állat 3", false, 10, Species.Dog));
    cage1.Add(new Animal("Állat 4", true,  10, Species.Dog));
    cage1.Add(new Animal("Állat 5", false, 10, Species.Panda));
    cage1.PrintContents();

    string filePath = "../../../template.txt";
    Console.WriteLine($"\nReading from file: {filePath}");
    Cage cage2 = new(filePath);
    cage2.PrintContents();
}