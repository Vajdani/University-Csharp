namespace SZFA_LAB03
{
    internal class Lodgings : Flat, IRent
    {
        int bookedMonths;

        public Lodgings(float area, int inhabitantsCount, int unitPrice) : base(area, 2, inhabitantsCount, unitPrice) { }

        public float GetCost(int months)
        {
            return TotalValue() / 240 / inhabitantsCount;
        }

        public bool IsBooked()
        {
            return bookedMonths > 0;
        }

        public bool Book(int months)
        {
            if (IsBooked())
            {
                return false;
            }

            bookedMonths = months;

            return true;
        }

        public override bool MoveIn(int newInhabitants)
        {
            int newTotal = inhabitantsCount + newInhabitants;
            if (!IsBooked() || newTotal >= 8 || area / newTotal < 2)
            {
                return false;
            }

            inhabitantsCount += newInhabitants;

            return true;
        }

        public override string ToString()
        {
            return $"[Lodgings] Area: {area}; Rooms: {roomsCount}; Renters: {inhabitantsCount}; Unit price: {unitPrice}; Booked months: {bookedMonths}";
        }
    }
}
