using NUnit.Framework;
using SZFA_LAB04_PrimeTool;

namespace SZFA_LAB04_Tests
{
    [TestFixture]
    public class PrimeToolTests
    {
        [TestCase(2,   true )]
        [TestCase(3,   true )]
        [TestCase(5,   true )]
        [TestCase(7,   true )]
        [TestCase(11,  true )]
        [TestCase(13,  true )]
        [TestCase(1,   false)]
        [TestCase(4,   false)]
        [TestCase(-13, false)]
        public void IsPrime(int number, bool expected)
        {
            //Arrange
            PrimeTool tool = new(number);

            //Act
            bool result = tool.IsPrime();

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        //[Test]
        //public void IsPrimeTrueForOneTest()
        //{
        //    //Arrange
        //    PrimeTool tool = new(1);

        //    //Act
        //    bool result = tool.IsPrime();

        //    //Assert
        //    Assert.That(result, Is.False);
        //}

        //[Test]
        //public void IsPrimeNegativeTest()
        //{
        //    //Arrange
        //    PrimeTool tool = new(-13);

        //    //Act
        //    bool result = tool.IsPrime();

        //    //Assert
        //    Assert.That(result, Is.False);
        //}
    }
}
