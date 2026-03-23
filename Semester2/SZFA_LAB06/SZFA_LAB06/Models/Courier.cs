using SZFA_LAB06.Exceptions;
using SZFA_LAB06.Interfaces;

namespace SZFA_LAB06.Models
{
    public class Courier
    {
        IDeliverable[] parcels;
        int parcelCount;

        public int TotalWeight {
            get
            {
                int sum = 0;
                foreach (IDeliverable parcel in parcels)
                {
                    if (parcel is not null)
                    {
                        sum += parcel.Weight;
                    }
                }

                return sum;
            }
        }

        public Courier(int parcelCount)
        {
            parcels = new Parcel[parcelCount];
        }

        public void PickUpItem(IDeliverable deliverable)
        {
            if (parcelCount >= parcels.Length)
            {
                throw new DeliveryException("A futár nem tud több csomagot szállítani.");
            }

            parcels[parcelCount++] = deliverable;
        }

        public IDeliverable[] FragileSorted()
        {
            List<FragileParcel> fragiles = [];
            foreach (IDeliverable deliverable in parcels)
            {
                if (deliverable is FragileParcel parcel)
                {
                    fragiles.Add(parcel);
                }
            }

            fragiles.Sort();

            return [.. fragiles];
        }
    }
}
