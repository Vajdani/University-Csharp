using NUnit.Framework;
using SZFA_LAB4;

namespace ProjectTester
{
    [TestFixture]
    public class PrimeToolTest
    {
        [TestCase(1,            false)]
        [TestCase(2,            true )]
        [TestCase(5,            true )]
        [TestCase(100,          false)]
        [TestCase(int.MaxValue, true )]
        [TestCase(int.MinValue, false)]
        public void PrimeTest(int prime, bool expected)
        {
            PrimeTool tool = new(prime);
            Assert.That(tool.IsPrime(), Is.EqualTo(expected));
        }
    }
}
