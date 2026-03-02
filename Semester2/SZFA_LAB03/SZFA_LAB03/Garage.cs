namespace SZFA_LAB03
{
    internal class Garage : IRealEstate, IRent
    {
        int area;
        int unitPrice;
        bool isHeated;
        int months;
        bool isOccupied;

        public Garage(int area, int unitPrice, bool isHeated)
        {
            this.area = area;
            this.unitPrice = unitPrice;
            this.isHeated = isHeated;
        }

        public int TotalValue()
        {
            Console.WriteLine($"{area} {unitPrice}");
            return area * unitPrice;
        }

        public int GetCost(int months)
        {
            return (int)Math.Round(TotalValue() / 120.0 / (isHeated ? 1.5 : 1) * months, 0);
        }

        public bool IsBooked()
        {
            return months > 0 || isOccupied;
        }

        public bool Book(int months)
        {
            if (IsBooked())
            {
                return false;
            }

            this.months = months;

            return true;
        }

        public void UpdateOccupied(bool occupied)
        {
            isOccupied = occupied;
        }

        public override string ToString()
        {
            return $"[Garage] Area: {area}; Unit price: {unitPrice}; {(isHeated ? "Heated" : "Not Heated")}; {(isOccupied ? "Occupied" : "Not occupied")}";
        }
    }
}
