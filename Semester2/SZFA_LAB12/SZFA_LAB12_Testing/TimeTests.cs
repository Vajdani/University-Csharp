using NUnit.Framework;
using SZFA_LAB12.Models;
using SZFA_LAB12.Exceptions;

namespace SZFA_LAB12_Testing
{
    [TestFixture]
    public class TimeTests
    {
        [Test]
        public void TimeParseMalformedInputTest()
        {
            // Arrange

            // Act

            // Assert
            Assert.Throws(typeof(TimeException), () => { Time.Parse("abcd:0:0"); });
            Assert.Throws(typeof(TimeException), () => { Time.Parse("0:abcd:0"); });
            Assert.Throws(typeof(TimeException), () => { Time.Parse("0:0:abcd"); });
        }

        [Test]
        public void TimeParseOutOfRangeInputTest()
        {
            // Arrange

            // Act

            // Assert
            Assert.Throws(typeof(TimeException), () => { Time.Parse("4:0:0"); });
            Assert.Throws(typeof(TimeException), () => { Time.Parse("-1:0:0"); });
            Assert.Throws(typeof(TimeException), () => { Time.Parse("0:60:0"); });
            Assert.Throws(typeof(TimeException), () => { Time.Parse("0:-1:0"); });
            Assert.Throws(typeof(TimeException), () => { Time.Parse("0:0:60"); });
            Assert.Throws(typeof(TimeException), () => { Time.Parse("0:0:-1"); });
        }

        [TestCase(0, 0,  0 )]
        [TestCase(1, 20, 30)]
        [TestCase(2, 5,  45)]
        [TestCase(3, 0,  1 )]
        public void TimeParseCorrectVerboseTest(int hours, int minutes, int seconds)
        {
            // Arrange
            Time time = Time.Parse($"{hours}:{minutes}:{seconds}");

            // Act

            // Assert
            Assert.That(time,    Is.Not.Null);
            Assert.That(hours,   Is.EqualTo(time.Hours));
            Assert.That(minutes, Is.EqualTo(time.Minutes));
            Assert.That(seconds, Is.EqualTo(time.Seconds));
        }

        [TestCase(0,  0 )]
        [TestCase(20, 30)]
        [TestCase(5,  45)]
        [TestCase(0,  1 )]
        public void TimeParseCorrectTest(int minutes, int seconds)
        {
            // Arrange
            Time time = Time.Parse($"{minutes}:{seconds}");

            // Act

            // Assert
            Assert.That(time,    Is.Not.Null);
            Assert.That(0,       Is.EqualTo(time.Hours));
            Assert.That(minutes, Is.EqualTo(time.Minutes));
            Assert.That(seconds, Is.EqualTo(time.Seconds));
        }

        [Test]
        public void TimeCompareTest()
        {
            // Arrange
            Time time1 = new(0, 1, 2);
            Time time2 = new(3, 4, 5);

            // Act

            // Assert
            Assert.That(0,  Is.EqualTo(time1.CompareTo(time1)));
            Assert.That(0,  Is.EqualTo(time2.CompareTo(time2)));
            Assert.That(1,  Is.EqualTo(time2.CompareTo(time1)));
            Assert.That(-1, Is.EqualTo(time1.CompareTo(time2)));
        }
    }
}
