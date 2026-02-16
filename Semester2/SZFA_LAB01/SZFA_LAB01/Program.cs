using SZFA_LAB01;

Cage testCage;
if (args.Length > 0)
{
    Console.WriteLine($"Folder arg detected: {args[0]}");

    Cage[] cages = Cage.MakeZooFromFolder(args[0]);
    Console.WriteLine($"Cages read from folder: {cages.Length}");
    foreach (Cage cage in cages)
    {
        cage.PrintContents();
    }

    testCage = cages[0];

    Console.WriteLine($"WhichCageContainsMostOf {Cage.WhichCageContainsMostOf(cages, Species.Dog)}");
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
    testCage = new(filePath);
    testCage.PrintContents();
}

Console.WriteLine();
testCage.PrintContents();
Console.WriteLine($"CountOfSpecies {testCage.CountOfSpecies(Species.Dog)}");
Console.WriteLine($"AnimalExists {testCage.AnimalExists(Species.Panda, true)}");
Console.WriteLine($"FindBySpecies {testCage.FindBySpecies(Species.Dog).Length}");
Console.WriteLine($"AverageWeightOfSpecies {testCage.AverageWeightOfSpecies(Species.Rabbit)}");
Console.WriteLine($"AnimalSexPairOfSpeciesExists {testCage.AnimalSexPairOfSpeciesExists(Species.Rabbit)}");

testCage.Delete("Pó");
testCage.Delete("Pó Apja");
testCage.PrintContents();
testCage.Add(new Animal("jdhjfdf", false, 0, 0));
testCage.Add(new Animal("jdhjfdf", false, 0, 0));
testCage.Add(new Animal("jdhjfdf", false, 0, 0));
testCage.PrintContents();