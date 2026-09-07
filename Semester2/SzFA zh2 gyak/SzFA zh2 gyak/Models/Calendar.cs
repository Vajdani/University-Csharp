namespace SzFA_zh2_gyak.Models
{
    public class Calendar
    {
        Day[] days;

        public Day[] Days { get => days; private set => days = value; }

        public Calendar(string[] inputs)
        {
            days = new Day[inputs.Length];

            for (int i = 0; i < inputs.Length; i++)
            {
                days[i] = Day.Parse(inputs[i]);
            }

            Array.Sort(days);
        }

        public Calendar(Day[] inputs)
        {
            days = inputs;

            Array.Sort(days);
        }

        public int GetDayIndex(DateTime date)
        {
            int left = 0;
            int right = days.Length - 1;
            int center = Center(left, right);
            while (left <= right && !days[center].Date.Equals(date))
            {
                if (days[center].Date.CompareTo(date) == 1)
                {
                    right = center - 1;
                }
                else
                {
                    left = center + 1;
                }

                center = Center(left, right);
            }

            if (left <= right)
            {
                return center;
            }

            return -1;
        }

        int LowerBoundSearch(DateTime date)
        {
            int left = 0;
            int right = days.Length - 1;
            int idx = days.Length;
            while (left <= right)
            {
                int center = Center(left, right);
                DateTime current = days[center].Date;
                if (current.CompareTo(date) != -1)
                {
                    idx = center;
                    right = center - 1;
                }
                else
                {
                    left = center + 1;
                }
            }

            return idx;
        }

        int UpperBoundSearch(DateTime date)
        {
            int left = 0;
            int right = days.Length - 1;
            int idx = days.Length;
            while (left <= right)
            {
                int center = Center(left, right);
                DateTime current = days[center].Date;
                if (current.CompareTo(date) == 1)
                {
                    idx = center;
                    right = center - 1;
                }
                else
                {
                    left = center + 1;
                }
            }

            return idx;
        }

        public PlannedTask[] AllTasks(DateTime firstDate, DateTime secondDate)
        {
            int lowerIdx = LowerBoundSearch(firstDate);
            int upperIdx = UpperBoundSearch(secondDate);

            List<PlannedTask> tasks = new(upperIdx - lowerIdx);
            for (int i = lowerIdx; i < upperIdx; i++)
            {
                foreach (PlannedTask task in days[i].Tasks)
                {
                    tasks.Add(task);   
                }
            }

            return [.. tasks];
        }

        int Center(int left, int right)
        {
            return (int)Math.Floor((left + right) * 0.5);
        }
    }
}
