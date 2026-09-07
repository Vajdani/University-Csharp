using SzFA_zh2_gyak.Models;
using SzFA_zh2_gyak.Exceptions;

namespace SzFA_zh2_gyak_Testing
{
    [TestFixture]
    public class PlannedTaskTests
    {
        [TestCase("Programozás ZH-ra felkészülni;130;7")]
        [TestCase("Analízis pótzh;80;10")]
        [TestCase("Elektro zh;120;4")]
        public void ParseCorrectTest(string input)
        {
            //Arrange

            //Act

            //Assert
            Assert.DoesNotThrow(() => PlannedTask.Parse(input));
        }

        [TestCase("")]
        [TestCase("Analízis pótzh;80")]
        [TestCase("Elektro zh;120;abcd")]
        public void ParseIncorrectTest(string input)
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws(typeof(PlannedTaskException), () => PlannedTask.Parse(input));
        }

        [TestCase("Programozás ZH-ra felkészülni;130;7", "Analízis pótzh;80;10", -1)]
        [TestCase("Analízis pótzh;60;10", "Analízis gyak;80;10", 1)]
        [TestCase("Elektro zh;120;4", "Elektro zh;120;4", 0)]
        public void ParseCorrectTest(string input1, string input2, int expected)
        {
            //Arrange
            PlannedTask task1 = PlannedTask.Parse(input1);
            PlannedTask task2 = PlannedTask.Parse(input2);

            //Act
            int result = task1.CompareTo(task2);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
