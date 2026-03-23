using SZFA_LAB06.Enums;

namespace SZFA_LAB06.Models
{
    public class NormalParcel : Parcel
    {
        public NormalParcel(int weight, string address) : base(weight, address, (Placement)Random.Shared.Next(0, Enum.GetValues(typeof(Placement)).Length)) {}

        public override double CalculatePrice(bool fromLocker)
        {
            if (fromLocker)
            {
                return 250 + Weight;
            }

            return 500 + Weight;
        }
    }
}
