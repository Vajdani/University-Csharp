namespace SZFA_LAB12.Models
{
    public class RunnerWithTime : IComparable
    {
        string name;
        Time time;

        public string Name { get => name; private set => name = value; }
        public Time Time { get => time; private set => time = value; }

        public RunnerWithTime(string name, Time time)
        {
            Name = name;
            Time = time;
        }

        public static RunnerWithTime Parse(string input)
        {
            string[] split = input.Split(',');
            if (split.Length != 2)
            {
                throw new ArgumentException();
            }

            return new(split[0], Time.Parse(split[1]));
        }

        public int CompareTo(object? obj)
        {
            if (obj is not RunnerWithTime runner)
            {
                return 1;
            }

            int timeComp = time.CompareTo(runner.Time);
            if (timeComp != 0)
            {
                return timeComp;
            }

            return name.CompareTo(runner.Name);
        }

        public override string ToString()
        {
            return $"{name} ({time})";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not RunnerWithTime runner)
            {
                return false;
            }

            return name == runner.Name && time.Equals(runner.Time);
        }
    }
}
