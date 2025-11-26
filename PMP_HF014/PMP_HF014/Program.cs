namespace PMP_HF014
{
    internal class Program
    {
        static long instructionIndex;
        static string[] program;

        static void Output(long count)
        {
            StreamWriter writer = new("output.txt");
            writer.Write(count);
            Console.Write(count);
            writer.Close();
        }

        static int RecurseProgram(string terminator)
        {
            int pathCount = 1;
            while (program[instructionIndex] != terminator)
            {
                if (program[instructionIndex] == "if")
                {
                    instructionIndex++;
                    pathCount *= RecurseProgram("else") + RecurseProgram("endif");
                }

                instructionIndex++;
            }

            return pathCount;
        }

        static void Main(string[] args)
        {
            StreamReader reader = new("input.txt");
            long rows = long.Parse(reader.ReadLine());

            program = new string[rows];
            for (int i = 0; i < rows; i++)
            {
                program[i] = reader.ReadLine();                
            }

            if (rows == 2 && program[0] == "begin" && program[1] == "end")
            {
                Output(1);
                return;
            }

            instructionIndex = 0;
            Output(RecurseProgram("end"));
        }
    }
}
