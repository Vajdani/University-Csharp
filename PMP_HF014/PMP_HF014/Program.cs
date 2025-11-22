namespace PMP_HF014
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader reader = new("input.txt");
            if (!int.TryParse(reader.ReadLine(), out int rows))
            {
                throw new Exception("Program length is not parseable as a number");
            }

            List<string> stack = new();
            int rowCount = 0;
            int blockCount = 0;
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
                    blockCount++;
                    stack.Add(line);
                }
                else if (line == "else")
                {
                    if (stack[^1] == "if")
                    {
                        stack.RemoveAt(stack.Count - 1);
                        stack.Add("else");
                    }
                    else
                    {
                        throw new Exception("Invalid block");
                    }
                }
                else if (line == "endif")
                {
                    Console.WriteLine($"\tSTACK ENDIF START: {string.Join(',', stack)}");

                    if (stack[^1] == "if" || stack[^1] == "else")
                    {
                        blockCount++;
                        stack.RemoveAt(stack.Count - 1);
                    }

                    Console.WriteLine($"\tSTACK ENDIF END: {string.Join(',', stack)}\n");
                }

                Console.WriteLine(line);
            }

            if (rowCount < rows)
            {
                throw new Exception("Program length is shorter than declared");
            }

            reader.Close();

            Console.WriteLine(blockCount);
        }
    }
}
