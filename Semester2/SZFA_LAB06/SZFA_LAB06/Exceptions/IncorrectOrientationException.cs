using SZFA_LAB06.Models;

namespace SZFA_LAB06.Exceptions
{
    public class IncorrectOrientationException : DeliveryException
    {
        Parcel parcel;
        public IncorrectOrientationException(string message, Parcel parcel) : base(message)
        {
            this.parcel = parcel;
        }
    }
}
