using SzFA_zh2_gyak.Models;

namespace SzFA_zh2_gyak_Testing
{
    [TestFixture]
    public class DayTests
    {
        [Test]
        public void ContainsTest()
        {
            //Arrange
            Day day = Day.Parse("2025.05.12.|Programozás ZH-ra felkészülni;130;7#Megnézni a Gyűrűk Ura trilógiát;449;7#Random meme-k keresése a neten;10;2");
            PlannedTask target = PlannedTask.Parse("Programozás ZH-ra felkészülni;130;7");

            //Act
            bool contains = day.Contains((PlannedTask task) => task.Equals(target), out PlannedTask found);

            //Arrange
            Assert.That(found, Is.Not.Null);
        }
    }
}