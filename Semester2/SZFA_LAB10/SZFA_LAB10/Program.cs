using SZFA_LAB10.Models;
using SZFA_LAB10.Enums;

OrderedItemsHandler itemHandler = new(
    [10, 5, 7, 20, 9, 3, 6, 2, 1, 30]
    //[1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
    //[10, 9, 8, 7, 6, 5, 4, 3, 2, 1]
);

Console.WriteLine($"isOrdered(isAscending = true): {(itemHandler.IsOrdered() ? "igen" : "nem")}");
Console.WriteLine($"isOrdered(isAscending = false): {(itemHandler.IsOrdered(false) ? "igen" : "nem")}");

Console.WriteLine("Sort with selection");
itemHandler.Sort(SortingMethod.Selection);

Console.WriteLine($"isOrdered(isAscending = true): {(itemHandler.IsOrdered() ? "igen" : "nem")}");

//itemHandler.Reverse();

Console.WriteLine($"isOrdered(isAscending = false): {(itemHandler.IsOrdered(false) ? "igen" : "nem")}");

Console.WriteLine(itemHandler.BinarySearch(20));
Console.WriteLine(itemHandler.BinarySearchRecursive(20));

Console.WriteLine(itemHandler.LowerBoundSearch(20));
Console.WriteLine(itemHandler.UpperBoundSearch(20));


Console.WriteLine("\nBooks:");
IComparable[] pBooks = [
    new PhoneBookItem("Xénia",        "0630-123-4567"),
    new PhoneBookItem("Péter",        "0630-123-3333"),
    new PhoneBookItem("Anasztázia",   "0630-123-4567"),
    new PhoneBookItem("Péter",        "0630-123-3333"),
    new PhoneBookItem("Dzsenifer",    "0630-123-4567"),
    new PhoneBookItem("Péter",        "0630-123-3333"),
    new PhoneBookItem("Bob",          "0630-123-4567"),
];

OrderedItemsHandler pBooksHandler = new(pBooks);

Console.WriteLine($"isOrdered(isAscending = true): {(pBooksHandler.IsOrdered() ? "igen" : "nem")}");
Console.WriteLine($"isOrdered(isAscending = false): {(pBooksHandler.IsOrdered(false) ? "igen" : "nem")}");

pBooksHandler.Sort(SortingMethod.Selection);

Console.WriteLine($"isOrdered(isAscending = true): {(pBooksHandler.IsOrdered() ? "igen" : "nem")}");
Console.WriteLine($"isOrdered(isAscending = false): {(pBooksHandler.IsOrdered(false) ? "igen" : "nem")}");

pBooksHandler.Sort(SortingMethod.Selection, false);

Console.WriteLine($"isOrdered(isAscending = true): {(pBooksHandler.IsOrdered() ? "igen" : "nem")}");
Console.WriteLine($"isOrdered(isAscending = false): {(pBooksHandler.IsOrdered(false) ? "igen" : "nem")}");