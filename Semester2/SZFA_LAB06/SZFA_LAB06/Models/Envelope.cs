using SZFA_LAB06.Exceptions;
using SZFA_LAB06.Interfaces;

namespace SZFA_LAB06.Models
{
    public class Envelope : IDeliverable
    {
        int weight;
        string address;
        string description;

        public int Weight { get => weight; set => weight = value; }
        public string Address { get => address; set => address = value; }

        public Envelope(int weight, string address, string description)
        {
            this.weight = weight;
            this.address = address;
            this.description = description;
        }

        public double CalculatePrice(bool fromLocker)
        {
            if (Weight <= 50)
            {
                return 200;
            }
            else if (Weight <= 500)
            {
                return 400;
            }
            else if (Weight <= 2000)
            {
                return 1000;
            }
            else
            {
                throw new OverweightException("A tömegnek kisebbnek kell lennie mint 2000 gramm!");
            }
        }

        public override string ToString()
        {
            return $"Címzett: {Address} / Leírás: {description} / Tömeg:{Weight} g";
        }
    }
}
