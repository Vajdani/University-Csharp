using SZFA_LAB05.Enums;
using SZFA_LAB05.Exceptions;

namespace SZFA_LAB05.Models
{
    public class Workout
    {
        SportType sportType;
        double distance;
        Time time;
        int elevation;
        int pulse;

        public Workout(SportType sportType, double distance, Time time, int elevation, int pulse)
        {
            this.sportType = sportType;
            this.distance = distance;
            this.time = time;
            this.elevation = elevation;
            this.pulse = pulse;
        }

        public override string ToString()
        {
            return $"{sportType}, D: {distance}, T: {time}, E: {elevation}, P: {pulse}";
        }

        public static Workout Parse(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                throw new WorkoutException("A megadott szöveg vagy null vagy üres.");
            }

            string[] split = input.Replace("\"", "").Split(',');
            if (split.Length < 5 || split.Length > 6)
            {
                throw new WorkoutException($"A megadott szöveg nem az elvárt formátumban van: 'type,distance,time,elevation,pulse'. Megadott: {input}");
            }

            string rawSportType = split[0];
            string rawDistance;
            string rawTime;
            string rawElevation;
            string rawPulse;
            if (split.Length == 6)
            {
                rawDistance = $"{split[1]},{split[2]}";
                rawTime = split[3];
                rawElevation = split[4];
                rawPulse = split[5];
            }
            else
            {
                rawDistance = split[1];
                rawTime = split[2];
                rawElevation = split[3];
                rawPulse = split[4];
            }

            if (!Enum.TryParse<SportType>(rawSportType, out SportType sportType))
            {
                throw new WorkoutException($"Ismeretlen sport típus: {rawSportType}, elfogadott értékek: {String.Join(',', Enum.GetValues<SportType>())}");
            }

            if (!double.TryParse(rawDistance, out double distance))
            {
                throw new WorkoutException($"A megadott távolság nem egy double: {rawDistance}");
            }

            if (!int.TryParse(rawElevation, out int elevation))
            {
                throw new WorkoutException($"A megadott emelkedés nem egy integer: {rawElevation}");
            }

            if (!int.TryParse(rawPulse, out int pulse))
            {
                throw new WorkoutException($"A megadott pulzus nem egy integer: {rawPulse}");
            }

            return new Workout(sportType, distance, Time.Parse(rawTime), elevation, pulse);
        }
    }
}
