using SzFA_zh2_gyak.Exceptions;
using SzFA_zh2_gyak.Models;

namespace SzFA_zh2_gyak
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Calendar calendar;

            try
            {
                calendar = ReadFolder("../../../Days/");
            }
            catch (PlannedTaskException ex)
            {
                Console.WriteLine("Hiba történt a fájlok feldolgozása során: " + ex.Message);
                return;
            }
            
            DateTime target = DateTime.Parse("2025.05.13");
            Console.WriteLine($"{target:yyyy:MM:dd} teendői:");
            foreach (PlannedTask task in calendar.Days[calendar.GetDayIndex(target)].Tasks)
            {
                Console.WriteLine($"\t{task}");
            }

            DateTime startDate = DateTime.Parse("2025.05.14");
            DateTime endDate = DateTime.Parse("2025.05.15.");
            Console.WriteLine($"Fontos teendők {startDate} és {endDate} között:");
            foreach (PlannedTask task in calendar.AllTasks(startDate, endDate))
            {
                if (task.Priority >= 5)
                {
                    Console.WriteLine($"\t{task}");
                }    
            }
        }

        static Day[] ReadFile(string path)
        {
            StreamReader reader = new(path, encoding:System.Text.Encoding.UTF8);

            try
            {
                int rows = int.Parse(reader.ReadLine());
                Day[] results = new Day[rows];

                for (int i = 0; i < rows; i++)
                {
                    results[i] = Day.Parse(reader.ReadLine());
                }

                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                reader.Close();

            }
        }

        static Calendar ReadFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                return null;
            }

            string[] files = Directory.GetFiles(path, "*.txt");
            List<Day> results = [];
            foreach (string file in files)
            {
                foreach(Day day in ReadFile(file))
                {
                    results.Add(day);
                }
            }

            return new Calendar([.. results]);
        }
    }
}
