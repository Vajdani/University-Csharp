using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SZFA_LAB03
{
    internal class Garage : IRealEstate, IRent
    {
        float area;
        int unitPrice;
        bool isHeated;
        int months;
        bool isOccupied;

        public Garage(float area, int unitPrice, bool isHeated)
        {
            this.area = area;
            this.unitPrice = unitPrice;
            this.isHeated = isHeated;
        }

        public float TotalValue()
        {
            return area * unitPrice;
        }

        public float GetCost(int months)
        {
            return (int)(TotalValue() / 120 / (isHeated ? 1.5 : 1));
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
