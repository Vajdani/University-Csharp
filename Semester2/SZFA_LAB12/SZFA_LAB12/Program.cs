using SZFA_LAB12.Exceptions;
using SZFA_LAB12.Models;

RaceResults ReadFile(string path)
{
    StreamReader reader = null;
    RaceResults results = null;

    try
    {
        reader = new StreamReader(path);
        string[] inputs = new string[int.Parse(reader.ReadLine()!)];

        int i = 0;
        while (!reader.EndOfStream)
        {
            inputs[i++] = reader.ReadLine()!;
        }

        results = new RaceResults(inputs.Length, inputs);
    }
    catch (TimeException ex)
    {
        Console.WriteLine(ex.Message);
        throw ex;
    }
    finally
    {
        reader.Close();
    }

    return results;
}

RaceResults[] ReadFolder(string path)
{
    string[] files = Directory.GetFiles(path, "*.txt");
    RaceResults[] results = new RaceResults[files.Length];

    for (int i = 0; i < results.Length; i++)
    {
        results[i] = ReadFile(files[i]);
    }

    return results;
}

