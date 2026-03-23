namespace SZFA_LAB06.Interfaces
{
    public interface IDeliverable
    {
        int Weight { get; set; }
        string Address { get; set; }

        double CalculatePrice(bool fromLocker);
    }
}
