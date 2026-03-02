using SZFA_LAB03;

InterfaceTest[] test = new InterfaceTest[10];
for (int i = 0; i < test.Length; i++)
{
    test[i] = new InterfaceTest();
}

foreach (InterfaceTest item in test)
{
    Console.WriteLine(item.order);
}

Array.Sort(test);

Console.WriteLine("\nSorted:");
foreach (InterfaceTest item in test)
{
    Console.WriteLine(item.order);
}

Console.WriteLine();

ApartmentHouse house = ApartmentHouse.LoadFromFile("import.txt");
Console.WriteLine($"Flats: {house.Flats.Length}; Garages: {house.Garages.Length}");
Console.WriteLine($"Total value: {house.TotalValue()}");