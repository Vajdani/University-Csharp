using SZFA_LAB05.Exceptions;

namespace SZFA_LAB05.Models
{
    public class Time
    {
        int hour;
        int minute;
        int second;

        public Time(int hour, int minute, int second)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }

        public override string ToString()
        {
            return $"Time: {hour}:{minute:00}:{second:00}";
        }

        public static Time Parse(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                throw new TimeException("A megadott szöveg vagy null vagy üres.");
            }

            string[] split = input.Trim('"').Split(':');
            if (split.Length != 3)
            {
                throw new TimeException($"A megadott szöveg nem az elvárt formátumban van: 'h:mm:ss'. Megadott: {input}");
            }

            if (!int.TryParse(split[0], out int hour) ||
                !int.TryParse(split[1], out int minute) ||
                !int.TryParse(split[2], out int second))
            {
                throw new TimeException($"Az időértékeknek számoknak kell lenniük. A hibás paraméter: {input}");
            }

            if (hour < 0)
            {
                throw new HourException("Az óra nem lehet negatív.");
            }

            if (minute < 0 || minute > 59)
            {
                throw new TimeException("A megadott percek száma nem tartozik a [0; 59] intervallumba.");
            }

            if (second < 0 || second > 59)
            {
                throw new TimeException("A megadott másodpercek száma nem tartozik a [0; 59] intervallumba.");
            }

            return new(hour, minute, second);
        }
    }
}
