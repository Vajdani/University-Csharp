string input = Console.ReadLine()!;
int offset = int.Parse(Console.ReadLine()!);
string output = "";
for (int i = 0; i < input.Length; i++)
{
    output += (char)((int)input[i] + offset);
}
Console.WriteLine(output);

Console.WriteLine();
while(true)
{
    int _offset = int.Parse(Console.ReadLine()!);
    string _output = "";
    for (int i = 0; i < output.Length; i++)
    {
        _output += (char)((int)output[i] + _offset);
    }
    Console.WriteLine(_output);
}