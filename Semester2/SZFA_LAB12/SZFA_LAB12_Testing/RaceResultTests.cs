using NUnit.Framework;
using SZFA_LAB12.Models;

namespace SZFA_LAB12_Testing
{
    [TestFixture]
    public class RaceResultTests
    {
        [Test]
        public void BetweenTest()
        {
            // Arrange
            string[] inputs = [
                "Jani,00:22:33",
                "Jani,00:22:34",
                "Zsuzsi,22:35",
                "Kati,50:35",
            ];
            Time lower = Time.Parse("22:00");
            Time upper = Time.Parse("25:00");
            RaceResults results = new(inputs.Length, inputs);

            // Act
            RunnerWithTime[] runners = results.Between(lower, upper);

            // Assert
            Assert.That(runners.Length, Is.EqualTo(3));
            Assert.That(runners[0], Is.EqualTo(RunnerWithTime.Parse("Jani,00:22:33")));
            Assert.That(runners[1], Is.EqualTo(RunnerWithTime.Parse("Jani,00:22:34")));
            Assert.That(runners[2], Is.EqualTo(RunnerWithTime.Parse("Zsuzsi,22:35")));
        }
    }
}
