namespace PMP_F1_ZH2
{
    class Spaceship
    {
        string name;
        ShipClassType type;
        int crewSize;
        CrewStatusType crewStatus;
        int cargoWeight;
        DateTime lastMessageDate;

        public string Name { get => name; }
        public int CrewSize { get => crewSize; }
        public ShipClassType Type { get => type; }
        public CrewStatusType CrewStatus { get => crewStatus; }
        public DateTime LastMessageDate { get => lastMessageDate; }
        public int CargoWeight { get => cargoWeight; }

        public Spaceship(string line)
        {
            string[] split = line.Split(';');
            name = split[0];
            type = Enum.Parse<ShipClassType>(split[1]);
            crewSize = int.Parse(split[2]);
            crewStatus = Enum.Parse<CrewStatusType>(split[3]);
            cargoWeight = int.Parse(split[4]);
            lastMessageDate = DateTime.Parse(split[5]);
        }

        int DaysSinceLastMessage(DateTime date)
        {
            return (int)(date - lastMessageDate).TotalDays;
        }

        public bool NeedsRescue(DateTime date)
        {
            return crewStatus switch
            {
                CrewStatusType.Active => DaysSinceLastMessage(date) > 30,
                CrewStatusType.InCryosleep => DaysSinceLastMessage(date) > 3650,
                CrewStatusType.MissingInAction => true,
                CrewStatusType.Deceased => true,
                _ => false,
            };
        }

        public string GetStatusReport(DateTime date)
        {
            return $"=== {this} ===\n  Crew: {crewSize} ({crewStatus})\n  Last message: {DaysSinceLastMessage(date)} days\n  Rescue needed: {(NeedsRescue(date) ? "Yes" : "No")}";
        }

        public bool IsRisky(DateTime date)
        {
            return type switch
            {
                ShipClassType.Cargo => NeedsRescue(date),
                ShipClassType.Research => NeedsRescue(date),
                _ => false,
            };
        }

        public override string ToString()
        {
            return $"{name} ({type})";
        }
    }
}
