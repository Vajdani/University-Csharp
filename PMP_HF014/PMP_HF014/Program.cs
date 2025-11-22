namespace PMP_HF014
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader reader = new("input.txt");
            if (!long.TryParse(reader.ReadLine(), out long rows))
            {
                throw new Exception("Program length is not parseable as a number");
            }

            long rowCount = 0;
            long pathCount = 0;
            while (!reader.EndOfStream)
            {
                rowCount++;
                if (rowCount > rows)
                {
                    throw new Exception("Program length is larger than declared");
                }

                string line = reader.ReadLine();
                if (rowCount == 1 && line != "begin" || rowCount == rows && line != "end")
                { 
                    throw new Exception("Invalid program");
                }

                if (line == "if")
                {
                    pathCount++;
                }
                else if(line == "else")
                {
                    pathCount++;
                    rowCount++;
                    reader.ReadLine();
                }
            }
            reader.Close();

            if (rowCount < rows)
            {
                throw new Exception("Program length is shorter than declared");
            }

            if (rowCount == 2)
            {
                pathCount++;
            }

            Console.WriteLine(pathCount);
        }
    }
}
