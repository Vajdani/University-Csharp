namespace PMP_F1_ZH2
{
    enum ShipClassType
    {
        Cargo, Military, Research, Mining, Colonial, Rescue
    }

    enum CrewStatusType
    {
        Active = 1, InCryosleep = 2, MissingInAction = 3, Deceased = 4
    }

    internal partial class Program
    {
        static void Main(string[] args)
        {
            FleetHandler fleet = new("weyland-yutani.csv");
            fleet.GenerateReport();
        }
    }
}
