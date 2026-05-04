using SZFA_LAB12.Exceptions;

namespace SZFA_LAB12.Models
{
    public class Time : IComparable
    {
        int hours;
        int minutes;
        int seconds;

        public int Hours
        {
            get => hours;
            set
            {
                if (value > 3)
                {
                    throw new TimeException("Hours must be less than 4.");
                }

                if (value < 0)
                {
                    throw new TimeException("Hours must be more than 0.");
                }

                hours = value;
            }
        }
        public int Minutes
        {
            get => minutes;
            set
            {
                if (value > 59)
                {
                    throw new TimeException("Minutes must be less than 60.");
                }

                if (value < 0)
                {
                    throw new TimeException("Minutes must be more than 0.");
                }

                minutes = value;
            }
        }
        public int Seconds
        {
            get => seconds;
            set
            {
                if (value > 59)
                {
                    throw new TimeException("Seconds must be less than 60.");
                }

                if (value < 0)
                {
                    throw new TimeException("Seconds must be more than 0.");
                }

                seconds = value;
            }
        }

        public Time(int hours, int minutes, int seconds)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        public Time(int minutes, int seconds) : this(0, minutes, seconds) { }

        public override string ToString()
        {
            if (hours == 0)
            {
                return $"{minutes:00}:{seconds:00}";
            }

            return $"{hours:00}:{minutes:00}:{seconds:00}";
        }

        public static Time Parse(string s)
        {
            string[] split = s.Split(":");

            switch (split.Length)
            {
                case 2:
                    if (!int.TryParse(split[0], out int minutes) ||
                        !int.TryParse(split[1], out int seconds))
                    {
                        throw new TimeException("Invalid time format.");
                    }

                    return new(0, minutes, seconds);
                case 3:
                    if (!int.TryParse(split[0], out int hours)    ||
                        !int.TryParse(split[1], out int minutes_) ||
                        !int.TryParse(split[2], out int seconds_))
                    {
                        throw new TimeException("Invalid time format.");
                    }

                    return new(hours, minutes_, seconds_);
                default:
                    throw new TimeException("Invalid time format.");
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Time time)
            {
                return false;
            }

            return time.Hours == hours && time.Minutes == minutes && time.Seconds == seconds;
        }

        public int CompareTo(object? obj)
        {
            if (obj is not Time time)
            {
                return 1;
            }

            int hourComp = hours.CompareTo(time.Hours);
            if (hourComp != 0)
            {
                return hourComp;
            }

            int minuteComp = minutes.CompareTo(time.Minutes);
            if (minuteComp != 0)
            {
                return minuteComp;
            }

            return seconds.CompareTo(time.Seconds);
        }
    }
}
