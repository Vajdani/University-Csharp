using HSZF_06_Practice.BusinessLogic.Services;
using HSZF_06_Practice.DataAccess.Interfaces;
using HSZF_06_Practice.DataAccess.Repositories;
using HSZF_06_Practice.Shared.Entities;
using Moq;
using NUnit.Framework;

namespace HSZF_06_Practice.Tests
{
    [TestFixture]
    public class MovieServiceTests
    {
        Mock<IMovieRepository> movieRepo;
        Mock<IRoleRepository> roleRepo;
        Mock<IDirectorRepository> directorRepo;
        MovieService sv;

        [SetUp]
        public void Init()
        {
            movieRepo = new Mock<IMovieRepository>();
            roleRepo = new Mock<IRoleRepository>();
            directorRepo = new Mock<IDirectorRepository>();
            sv = new(movieRepo.Object, roleRepo.Object, directorRepo.Object);
        }

        [Test]
        public void GetAll()
        {
            //Arrange
            List<Movie> expectedItems = [
                new Movie(1, "First", 555.1, 1, new DateTime(2026, 1, 1), 5),
                new Movie(2, "Second", 33, 1, new DateTime(2026, 1, 1), 5),
                new Movie(3, "Third", 44.5, 2, new DateTime(2026, 1, 1), 5),
            ];

            movieRepo.Setup(x => x.GetAll()).Returns(expectedItems.AsQueryable());

            //Act
            var result = sv.GetAll();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(expectedItems.Count));
            CollectionAssert.AreEquivalent(result, expectedItems);
            movieRepo.Verify(x => x.GetAll(), Times.Once);
            movieRepo.VerifyNoOtherCalls();
            roleRepo.VerifyNoOtherCalls();
            directorRepo.VerifyNoOtherCalls();
        }


        [TestCase("Simple title", 500.2, 2026, 6, 1)]
        public void CreateWithSuccessTest(string title, double income, int year, double rating, int directorId)
        {
            //Arrange
            Movie entity = new() {
                Title = title,
                Income = income,
                Release = new DateTime(year, 1, 1),
                Rating = rating,
                DirectorId = directorId
            };

            var id = entity.Id;
            var newId = Random.Shared.Next(1, 1001);
            movieRepo.Setup(x => x.Create(It.IsAny<Movie>())).Returns(() =>
            {
                return new Movie(newId, title, income, directorId, entity.Release, rating);
            });

            //Act
            sv.Create(entity);

            //Assert
            Assert.That(id, Is.Not.EqualTo(entity.Id));
            Assert.That(newId, Is.EqualTo(entity.Id));
            movieRepo.Verify(x => x.Create(It.IsAny<Movie>()), Times.Once);
            movieRepo.VerifyNoOtherCalls();
        }
    }
}
