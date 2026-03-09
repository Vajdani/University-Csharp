using NUnit.Framework;
using SZFA_LAB04_IntArrayStat;

namespace SZFA_LAB04_Tests
{
    [TestFixture]
    internal class ArrayStatisticsTests
    {
        [TestCase(new int[0], 0)]
        [TestCase(new int[1] { 1 }, 1)]
        [TestCase(new int[4] { 1, 2, 3, 4 }, 10)]
        [TestCase(new int[4] { -1, -2, 3, 4 }, 4)]
        public void TotalTest(int[] numbers, int expected)
        {
            //Arrange
            ArrayStatistics stat = new(numbers);

            //Act
            int result = stat.Total();

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(new int[0], 0, false)]
        [TestCase(new int[1] { 1 }, 1, true)]
        [TestCase(new int[4] { 1, 2, 3, 4 }, 10, false)]
        [TestCase(new int[4] { -1, -2, 3, 4 }, 4, true)]
        public void ContainsTest(int[] numbers, int number, bool expected)
        {
            //Arrange
            ArrayStatistics stat = new(numbers);

            //Act
            bool result = stat.Contains(number);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(new int[0], true)]
        [TestCase(new int[4] { 1, 2, 3, 4 }, true)]
        [TestCase(new int[7] { 1, 1, 2, 2, 2, 3, 4 }, true)]
        [TestCase(new int[4] { -1, -2, 3, 4 }, false)]
        [TestCase(new int[4] { -1, 3, -2, 4 }, false)]
        public void SortedTest(int[] numbers, bool expected)
        {
            //Arrange
            ArrayStatistics stat = new(numbers);

            //Act
            bool result = stat.Sorted();

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(new int[0], 13, -1)]
        [TestCase(new int[1] { 1 }, 1, -1)]
        [TestCase(new int[1] { 1 }, 0, 0)]
        [TestCase(new int[4] { 1, 2, 3, 4 }, 10, -1)]
        [TestCase(new int[4] { 1, 2, 3, 4 }, 0, 0)]
        [TestCase(new int[4] { 1, 2, 3, 4 }, 1, 1)]
        [TestCase(new int[4] { 1, 2, 7, 8 }, 5, 2)]
        [TestCase(new int[4] { 8, 3, 7, 10 }, 9, 3)]
        public void FirstGreaterTest(int[] numbers, int number, int expected)
        {
            //Arrange
            ArrayStatistics stat = new(numbers);

            //Act
            int result = stat.FirstGreater(number);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
