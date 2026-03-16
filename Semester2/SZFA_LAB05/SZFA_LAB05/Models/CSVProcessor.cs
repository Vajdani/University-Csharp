using SZFA_LAB05.Exceptions;

namespace SZFA_LAB05.Models
{
    public class CSVProcessor
    {
        string filePath;

        public CSVProcessor(string filePath)
        {
            this.filePath = filePath;
        }

        public Workout[] AllItems()
        {
            StreamReader reader = null;
            List<Workout> workouts = [];
            try
            {
                reader = new(filePath);
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine()!;
                    try
                    {
                        workouts.Add(Workout.Parse(line));
                    }
                    catch (HourException hex)
                    {
                        Console.WriteLine(hex.Message);
                    }
                    catch (TimeException tex)
                    {
                        Console.WriteLine(tex.Message);
                    }
                    catch (WorkoutException wex)
                    {
                        Console.WriteLine(wex.Message);
                    }
                }
            }
            finally
            {
                reader.Close();
            }

            return [.. workouts];
        }
    }
}
