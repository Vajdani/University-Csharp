namespace SZFA_LAB03
{
    internal class FamilyApartment : Flat
    {
        int childrenCount;

        public FamilyApartment(int area, int roomsCount, int inhabitantsCount, int unitPrice) : base(area, roomsCount, inhabitantsCount, unitPrice) { }

        public bool ChildIsBorn()
        {
            if ((inhabitantsCount - childrenCount) < 2)
            {
                return false;
            }

            inhabitantsCount++;
            childrenCount++;

            return true;
        }

        public override bool MoveIn(int newInhabitants)
        {
            int newTotal = inhabitantsCount + newInhabitants;
            if (newTotal / roomsCount > 2 || (newTotal - childrenCount) * 10 + childrenCount * 5 > area)
            {
                return false;
            }

            inhabitantsCount = newTotal;

            return true;
        }
    }
}
