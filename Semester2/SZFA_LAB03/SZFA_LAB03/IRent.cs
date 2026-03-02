namespace SZFA_LAB03
{
    internal interface IRent
    {
        int GetCost(int months);
        bool IsBooked();
        bool Book(int months);
    }
}
