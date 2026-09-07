using SzFA_zh2_gyak.Exceptions;

namespace SzFA_zh2_gyak.Models
{
    public class PlannedTask : IComparable
    {
        string name;
        int length;
        int priority;

        public string Name { get => name; set => name = value; }
        public int Length
        {
            get => length;
            set
            {
                if (value < 1 || value > 1440)
                    throw new PlannedTaskException("hossz hiba");

                length = value;
            }
        }
        public int Priority
        {
            get => priority;
            set
            {
                if (value < 1 || value > 10)
                    throw new PlannedTaskException("prioritás hiba");

                priority = value;
            }
        }

        public PlannedTask(string name, int length, int priority)
        {
            Name = name;
            Length = length;
            Priority = priority;
        }

        public PlannedTask(string name, int length) : this(name, length, 5) { }

        public override string ToString()
        {
            return $"[P:{priority}] {name} ({length} perc)";
        }

        public static PlannedTask Parse(string input)
        {
            string[] split = input.Split(';');
            if (split.Length != 3)
                throw new PlannedTaskException("parse hiba");

            if (!int.TryParse(split[1], out int length) ||
                !int.TryParse(split[2], out int priority))
                throw new PlannedTaskException("parse hiba");

            return new PlannedTask(split[0], length, priority);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not PlannedTask task) return false;

            return name == task.name && length == task.length && priority == task.priority;
        }

        public int CompareTo(object? obj)
        {
            if (obj is not PlannedTask task) return 1;
            if (Equals(obj)) return 0;

            int priorityComp = priority.CompareTo(task.priority);
            if (priorityComp != 0) return priorityComp;

            return task.length.CompareTo(length);
        }
    }
}
