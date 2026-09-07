namespace SzFA_zh2_gyak.Models
{
    public class Day : IComparable
    {
        DateTime date;
        PlannedTask[] tasks;

        public DateTime Date { get => date; private set => date = value; }
        public PlannedTask[] Tasks { get => tasks; private set => tasks = value; }

        public Day(DateTime date, PlannedTask[] tasks)
        {
            Date = date;
            Tasks = tasks;

            if (!IsSorted())
            {
                Sort();
            }
        }

        private bool IsSorted()
        {
            int i = 0;
            while (i < tasks.Length - 1 && tasks[i].CompareTo(tasks[i + 1]) == -1)
            {
                i++;
            }

            return i == tasks.Length;
        }

        private void Sort()
        {
            int i = tasks.Length - 1;
            while (i > 1)
            {
                int idx = -1;
                for (int j = 0; j < i - 1; j++)
                {
                    if (tasks[j].CompareTo(tasks[j + 1]) == 1)
                    {
                        (tasks[j], tasks[j + 1]) = (tasks[j + 1], tasks[j]);
                        idx = j;
                    }
                }

                i = idx;
            }
        }

        public static Day Parse(string input)
        {
            string[] split = input.Split('|');
            string[] tasksSplit = split[1].Split('#');
            
            PlannedTask[] tasks = new PlannedTask[tasksSplit.Length];
            for (int i = 0; i < tasksSplit.Length; i++)
            {
                tasks[i] = PlannedTask.Parse(tasksSplit[i]);
            }

            return new Day(DateTime.Parse(split[0]), tasks);
        }

        public bool Contains(Predicate<PlannedTask> predicate, out PlannedTask searchedTask)
        {
            int i = 0;
            while (i < tasks.Length && !predicate(tasks[i]))
            {
                i++;
            }

            if (i < tasks.Length)
            {
                searchedTask = tasks[i];
                return true;
            }

            searchedTask = null;
            return false;
        }

        public int CompareTo(object? obj)
        {
            if (obj is not Day day) return 1;

            return date.CompareTo(day.date);
        }

        public override string ToString()
        {
            return $"{date:yyyy.MM.dd.}\n{String.Join('\n', (IEnumerable<PlannedTask>)tasks)}";
        }
    }
}
