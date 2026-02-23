using System.Drawing;
using SZFA_LAB02;

Shape[] shapes = {
    new SZFA_LAB02.Rectangle(5, 10),
    new Square(20),
    new Circle(25),
    new Circle(10),
    new SZFA_LAB02.Rectangle(5, 10)
};

void MakeHoley(Shape shape)
{
    if (shape.Area() > shape.Perimeter())
    {
        shape.MakeHoley();
    }
}

Shape MakeRect(int height, int width)
{
    if (height == width)
    {
        return new Square(width);
    }

    return new SZFA_LAB02.Rectangle(height, width);
}

Shape FindLargestShape(Shape[] shapes)
{
    int max = 0;
    double lastArea = shapes[0].Area();
    for (int i = 1; i < shapes.Length; i++)
    {
        double newArea = shapes[i].Area();
        if (newArea > lastArea)
        {
            max = i;
            lastArea = newArea;
        }
    }

    return shapes[max];
}

void PrintShapes(Shape[] shapes)
{
    foreach (Shape shape in shapes)
    {
        Console.WriteLine(shape);
    }
}



PrintShapes(shapes);

Console.WriteLine("\nMakeHoley");
foreach (Shape shape in shapes)
{
    MakeHoley(shape);
}

PrintShapes(shapes);

Console.WriteLine("\nMakeRect");
Console.WriteLine(MakeRect(10, 20));
Console.WriteLine(MakeRect(10, 10));

Console.WriteLine("\nFindLargestShape");
Console.WriteLine(FindLargestShape(shapes));

Console.WriteLine("\nCopyTest");
SZFA_LAB02.Rectangle rect1 = new(10, 20, Color.AliceBlue, true);
SZFA_LAB02.Rectangle rect2 = new(25, 50);
Console.WriteLine(rect1);
Console.WriteLine(rect2);
rect2.Copy(rect1);
Console.WriteLine("Copied:");
Console.WriteLine(rect2);

Square sqr1 = new(60, Color.MintCream, true);
Square sqr2 = new(75);
Console.WriteLine();
Console.WriteLine(sqr1);
Console.WriteLine(sqr2);
sqr2.Copy(sqr1);
Console.WriteLine("Copied:");
Console.WriteLine(sqr2);

Circle crc1 = new(60, Color.Red, true);
Circle crc2 = new(75);
Console.WriteLine();
Console.WriteLine(crc1);
Console.WriteLine(crc2);
sqr2.Copy(crc1);
Console.WriteLine("Copied:");
Console.WriteLine(crc2);