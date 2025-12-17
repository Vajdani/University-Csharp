using System;

namespace PMP_F1_ZH2
{
    internal class FleetHandler
    {
        List<Spaceship> ships;
        DateTime today;

        public int TotalShipCount => ships.Count;


        public FleetHandler(string fileName)
        {
            StreamReader reader = new(fileName);
            today = DateTime.Parse(reader.ReadLine());

            ships = new();
            while (!reader.EndOfStream)
            {
                ships.Add(new Spaceship(reader.ReadLine()));
            }
        }

        public bool HasAnyDeceasedCrew()
        {
            for (int i = 0; i < ships.Count; i++)
            {
                if (ships[i].CrewStatus == CrewStatusType.Deceased)
                {
                    return true;
                }
            }

            return false;
        }

        public double AverageCargo(ShipClassType shipClass)
        {
            double sum = 0;
            int count = 0;
            for (int i = 0; i < ships.Count; i++)
            {
                if (ships[i].Type == shipClass)
                {
                    sum += ships[i].CargoWeight;
                    count++;
                }
            }

            return sum / count;
        }

        public Spaceship[] GetShipsGroupByRisk()
        {
            Spaceship[] ordered = ships.ToArray();
            for (int i = 0; i < TotalShipCount - 1; i++)
            {
                for (int j = 0; j < TotalShipCount - 1 - i; j++)
                {
                    if (!ordered[j].IsRisky(today) && ordered[j + 1].IsRisky(today))
                    {
                        (ordered[j], ordered[j + 1]) = (ordered[j + 1], ordered[j]);
                    }
                }
            }

            return ordered;
        }

        public string GetLargestCrewData(CrewStatusType status)
        {
            int max = -1;
            for (int i = 0; i < ships.Count; i++)
            {
                if (ships[i].CrewStatus == status && (max == -1 || ships[max].CrewSize < ships[i].CrewSize))
                {
                    max = i;
                }
            }

            if (max == -1)
            {
                return "";
            }

            return ships[max].GetStatusReport(today);
        }

        public void GenerateReport()
        {
            Console.WriteLine($"Current date is {today:yyyy. MM. dd. H:mm:ss}");
            Console.WriteLine($"Total ship count is {TotalShipCount}");

            Console.WriteLine("Average cargo by ship types:");
            foreach (ShipClassType type in Enum.GetValues<ShipClassType>())
            {
                Console.WriteLine($"  {type}: {AverageCargo(type)}");
            }

            Console.WriteLine("Detailed info sorted by risk:");
            foreach (Spaceship ship in GetShipsGroupByRisk())
            {
                Console.WriteLine(ship.GetStatusReport(today));
            }
        }
    }
}
