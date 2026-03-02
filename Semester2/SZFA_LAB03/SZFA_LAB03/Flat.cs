namespace SZFA_LAB03
{
    internal abstract class Flat : IRealEstate
    {
        protected float area;
        protected int roomsCount;
        protected int inhabitantsCount;
        protected int unitPrice;

        public int InhabitantCount { get => inhabitantsCount; }

        public Flat(float area, int roomsCount, int inhabitantsCount, int unitPrice)
        {
            this.area = area;
            this.roomsCount = roomsCount;
            this.inhabitantsCount = inhabitantsCount;
            this.unitPrice = unitPrice;
        }

        public abstract bool MoveIn(int newInhabitants);

        public float TotalValue()
        {
            return area * unitPrice;
        }

        public override string ToString()
        {
            return $"[Flat] Area: {area}; Rooms: {roomsCount}; Renters: {inhabitantsCount}; Unit price: {unitPrice}";
        }
    }
}
