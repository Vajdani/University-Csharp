namespace SZFA_LAB03
{
    internal abstract class Flat : IRealEstate
    {
        protected int area;
        protected int roomsCount;
        protected int inhabitantsCount;
        protected int unitPrice;

        public int InhabitantCount { get => inhabitantsCount; }

        public Flat(int area, int roomsCount, int inhabitantsCount, int unitPrice)
        {
            this.area = area;
            this.roomsCount = roomsCount;
            this.inhabitantsCount = inhabitantsCount;
            this.unitPrice = unitPrice;
        }

        public abstract bool MoveIn(int newInhabitants);

        public int TotalValue()
        {
            Console.WriteLine($"{area} {unitPrice}");
            return area * unitPrice;
        }

        public override string ToString()
        {
            return $"[Flat] Area: {area}; Rooms: {roomsCount}; Renters: {inhabitantsCount}; Unit price: {unitPrice}";
        }
    }
}
