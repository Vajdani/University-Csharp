using SZFA_LAB06.Enums;
using SZFA_LAB06.Interfaces;

namespace SZFA_LAB06.Models
{
    public abstract class Parcel : IDeliverable, IComparable
    {
        int weight;
        string address;
        Placement placement;

        public int Weight { get => weight; set => weight = value; }
        public string Address { get => address; set => address = value; }

        public Parcel(int weight, string address, Placement placement) : this(weight, address)
        {
            this.placement = placement;
        }

        public Parcel(int weight, string address)
        {
            this.weight = weight;
            this.address = address;
        }

        public abstract double CalculatePrice(bool fromLocker);

        public override string ToString()
        {
            return $"Címzett: {Address} / Elhelyezés: {placement} / Tömeg:{Weight} g";
        }

        public int CompareTo(object? obj)
        {
            if (obj is null || obj is not Parcel parcel)
            {
                return 1;
            }

            return weight.CompareTo(parcel.Weight);
        }
    }
}
