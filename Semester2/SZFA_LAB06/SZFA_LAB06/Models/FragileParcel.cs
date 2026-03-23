using SZFA_LAB06.Enums;
using SZFA_LAB06.Exceptions;

namespace SZFA_LAB06.Models
{
    public class FragileParcel : Parcel
    {
        public FragileParcel(int weight, string address, Placement placement) : base(weight, address, placement)
        {
            if (placement == Placement.Arbitrary)
            {
                throw new IncorrectOrientationException("A törékeny csomagokat nem lehet bárhogy elhejezni.", this);
            }
        }

        public override double CalculatePrice(bool fromLocker)
        {
            if (fromLocker)
            {
                throw new DeliveryException("A törékeny csomagokat nem lehet automatából feladni!");
            }

            return 1000 + Weight * 2;
        }
    }
}
