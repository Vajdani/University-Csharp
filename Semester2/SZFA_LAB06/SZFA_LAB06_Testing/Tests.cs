using NUnit.Framework;
using SZFA_LAB06.Enums;
using SZFA_LAB06.Exceptions;
using SZFA_LAB06.Interfaces;
using SZFA_LAB06.Models;

namespace SZFA_LAB06_Testing
{
    [TestFixture]
    public class Tests
    {
        [TestCase(50, false, 200)]
        [TestCase(500, false, 400)]
        [TestCase(2000, false, 1000)]
        public void EnvelopeTest(int weight, bool fromLocker, int expected)
        {
            Envelope envelope = new(weight, "test", "test");

            double result = envelope.CalculatePrice(fromLocker);

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(4000, false)]
        [TestCase(2001, false)]
        public void EnvelopeErrorTest(int weight, bool fromLocker)
        {
            Envelope envelope = new(weight, "test", "test");

            Assert.Throws<OverweightException>(() => envelope.CalculatePrice(fromLocker));
        }

        [TestCase(50, Placement.Horizontal)]
        public void FragileParcelPriceErrorTest(int weight, Placement placement)
        {
            FragileParcel parcel = new(weight, "test", placement);

            Assert.Throws<DeliveryException>(() => parcel.CalculatePrice(true));
        }

        [TestCase(50)]
        public void FragileParcelConstructorErrorTest(int weight)
        {
            Assert.Catch<IncorrectOrientationException>(() => new FragileParcel(weight, "test", Placement.Arbitrary));
        }

        [TestCase(4, 2)]
        [TestCase(4, 4)]
        [TestCase(4, 1)]
        public void CourierPickUpTest(int maxParcelCount, int parcelCount)
        {
            Courier courier = new(maxParcelCount);

            for (int i = 0; i < parcelCount; i++)
            {
                courier.PickUpItem(new NormalParcel(50, "test"));
            }

            Assert.That(courier.TotalWeight, Is.EqualTo(parcelCount * 50));
        }

        public void CourierSortedTest(int maxParcelCount, int fragileCount)
        {
            Courier courier = new(maxParcelCount);

            int fragileAdded = 0;
            for (int i = 0; i < maxParcelCount; i++)
            {
                if (fragileAdded < fragileCount)
                {
                    courier.PickUpItem(new FragileParcel(Random.Shared.Next(10, 101), "test", Placement.Horizontal));
                }
                else
                {
                    courier.PickUpItem(new NormalParcel(50, "test"));
                }
            }

            IDeliverable[] fragileParcels = courier.FragileSorted();

            Assert.That(fragileParcels.Length, Is.EqualTo(fragileCount));

            for (int i = 0; i < fragileParcels.Length - 1; i++)
            {
                Assert.That(((FragileParcel)fragileParcels[i]).CompareTo((FragileParcel)fragileParcels[i + 1]), Is.EqualTo(1));
            }
        }
    }
}
