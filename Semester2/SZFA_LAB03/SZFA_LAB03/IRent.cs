namespace SZFA_LAB03
{
    internal interface IRent
    {
        float GetCost(int months);
        bool IsBooked();
        bool Book(int months);
    }
}
