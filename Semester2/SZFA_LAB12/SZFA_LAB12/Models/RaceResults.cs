namespace SZFA_LAB12.Models
{
    public class RaceResults
    {
        RunnerWithTime[] results;

        public RaceResults(int runnersCount, string[] inputs)
        {
            results = new RunnerWithTime[runnersCount];

            for (int i = 0; i < runnersCount; i++)
            {
                results[i] = RunnerWithTime.Parse(inputs[i]);
            }
            
            if (!IsSorted())
            {
                Sort();
            }
        }

        bool IsSorted()
        {
            int i = 0;
            while (i < results.Length - 1 && results[i].Time.CompareTo(results[i + 1].Time) != 1)
            {
                i++;
            }

            return i == results.Length - 1;
        }

        void Sort()
        {
            for (int i = 1; i < results.Length; i++)
            {
                int j = i - 1;
                RunnerWithTime cur = results[i];
                
                while (j >= 0 && results[j].Time.CompareTo(cur.Time) == 1)
                {
                    results[j + 1] = results[j];
                    j--;
                }

                results[j + 1] = cur;
            }
        }

        int LowerBound(Time time)
        {
            int left = 0;
            int right = results.Length - 1;
            int idx = results.Length;
            while (left <= right)
            {
                int center = GetCenter(left, right);
                if (results[center].Time.CompareTo(time) != -1)
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

        int UpperBound(Time time)
        {
            int left = 0;
            int right = results.Length - 1;
            int idx = results.Length;
            while (left <= right)
            {
                int center = GetCenter(left, right);
                if (results[center].Time.CompareTo(time) == 1)
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

        public RunnerWithTime[] Between(Time lower, Time upper)
        {
            int lowerBound = LowerBound(lower);
            int upperBound = UpperBound(upper);

            RunnerWithTime[] runners = new RunnerWithTime[upperBound - lowerBound];
            for (int i = 0; i < runners.Length; i++)
            {
                runners[i] = results[lowerBound + i];                
            }

            return runners;
        }

        public bool Contains(Predicate<RunnerWithTime> predicate, out RunnerWithTime runnerPerformance)
        {
            int i = 0;
            while (i < results.Length && !predicate(results[i]))
            {
                i++;
            }

            if (i < results.Length)
            {
                runnerPerformance = results[i];
                return true;
            }

            runnerPerformance = null;
            return false;
        }

        private static int GetCenter(int left, int right)
        {
            return (int)Math.Floor((left + right) / 2.0);
        }
    }
}
