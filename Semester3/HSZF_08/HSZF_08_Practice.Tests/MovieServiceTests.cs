using HSZF_08_Practice.BusinessLogic.Models;
using HSZF_08_Practice.BusinessLogic.Services;
using HSZF_08_Practice.DataAccess.Interfaces;
using HSZF_08_Practice.Shared.Entities;
using Moq;
using NUnit.Framework;

namespace HSZF_08_Practice.Tests
{
    [TestFixture]
    public class MovieServiceTests
    {
        Mock<IMovieRepository> movieRepository;
        Mock<IRoleRepository> roleRepository;
        Mock<IDirectorRepository> directorRepository;

        MovieService service;

        [SetUp]
        public void Init()
        {
            // This method can be used to set up any common test data or configurations before each test runs.
            movieRepository = new Mock<IMovieRepository>();
            roleRepository = new Mock<IRoleRepository>();
            directorRepository = new Mock<IDirectorRepository>();
            service = new MovieService(movieRepository.Object, roleRepository.Object, directorRepository.Object);
        }

        [Test]
        public void GetAllTest()
        {
            // Arrange
            var expectedMovies = new List<Movie>()
            {
                new Movie { Id = 1, Title = "Movie 1", Income = 1000000, Rating = 8.5, Release = new DateTime(2020, 1, 1), DirectorId = 1 },
                new Movie { Id = 2, Title = "Movie 2", Income = 2000000, Rating = 7.5, Release = new DateTime(2021, 1, 1), DirectorId = 2 }
            };

            movieRepository.Setup(m => m.GetAll()).Returns(expectedMovies.AsQueryable());

            // Act
            var result = service.GetAll();

            // Assert
            Assert.That(result.Count, Is.EqualTo(expectedMovies.Count));
            CollectionAssert.AreEqual(expectedMovies, result); // This checks also the same count
            movieRepository.Verify(x => x.GetAll(), Times.Once);
            movieRepository.VerifyNoOtherCalls();
            roleRepository.VerifyNoOtherCalls();
            directorRepository.VerifyNoOtherCalls();
        }

        [TestCase("a", 2026, 0, 1, 2)]
        [TestCase("aasdf as fasf asf asf asf dasfd asdf asdf asdf asdf asdf asdf asdf asdf as fdasdf safd sad fsadf sa fdaasdf as fasf asf asf asf dasfd asdf asdf asdf asdf asdf asdf asdf asdf as fdasdf safd sad fsadf sa fdsdfgsgfdsgsdfgsdgertwertweertret", 2026, 10000, 10, 1)]
        public void CreateWithSuccessTest(string title, int releaseYear, double income, double rating, int directorId)
        {
            // Arrange
            // Simple model for create
            var movieForCreate = new Movie
            {
                Title = title,
                Release = new DateTime(releaseYear, 1, 1),
                Income = income,
                Rating = rating,
                DirectorId = directorId
            };
            int movieIdBeforeCreate = movieForCreate.Id;

            // Imitate repo behaviour
            var nextId = new Random().Next(1, 1000);
            movieRepository.Setup(m => m.Create(It.IsAny<Movie>())).Returns<Movie>(m =>
            {
                movieForCreate.Id = nextId;
                return movieForCreate;
            });
            directorRepository.Setup(x => x.Exists(It.IsNotIn(GetTheoreticalDirectorIds()))).Returns(false);
            directorRepository.Setup(x => x.Exists(It.IsIn(GetTheoreticalDirectorIds()))).Returns(true);

            // Act
            service.Create(movieForCreate); // This will call the repository's Create method, which will set the Id of movieForCreate

            // Assert
            Assert.That(movieForCreate.Id, Is.Not.EqualTo(movieIdBeforeCreate));
            movieRepository.Verify(m => m.Create(It.IsAny<Movie>()), Times.Once);
            movieRepository.VerifyNoOtherCalls();
            roleRepository.VerifyNoOtherCalls();
            directorRepository.Verify(x => x.Exists(movieForCreate.DirectorId), Times.Once);
            directorRepository.VerifyNoOtherCalls();
        }

        private IEnumerable<int> GetTheoreticalDirectorIds()
        {
            return Enumerable.Range(1, 18);
        }

        [TestCase("", 2026, 0, 1, 2, "Movie title cannot be empty.")]
        [TestCase("aasdf as fasf asf asf asf dasfd asdf asdf asdf asdf asdf asdf asdf asdf as fdasdf safd sad fsadf sa fdaasdf as fasf asf asf asf dasfd asdf asdf asdf asdf asdf asdf asdf asdf as fdasdf safd sad fsadf sa fdsdfgsgfdsgsdfgsdgertwertweertret123456789012345678901234567890", 2026, 10000, 10, 1, "Movie title cannot be longer than 256 characters.")]
        [TestCase(null, 2026, 10000, 10, 1, "Movie title cannot be empty.")]
        [TestCase(null, 2026, 10000, 10, 1, "Movie title cannot be empty.")]
        [TestCase("year fail", 200, 0, 10, 1, "Movie release year must be greater than 1888.")]
        [TestCase("income fail", 2026, -1, 10, 1, "Movie income must be between 0 and 10000.")]
        [TestCase("income fail", 2026, 10001, 10, 1, "Movie income must be between 0 and 10000.")]
        [TestCase("rating fail", 2026, 123, -1, 1, "Movie rating must be between 1 and 10.")]
        [TestCase("rating fail", 2026, 123, 13, 1, "Movie rating must be between 1 and 10.")]
        [TestCase("director fail", 2026, 123, 10, 0, "Director does not exist with id: 0.")]
        public void CreateWithExceptionTest(string title, int releaseYear, double income, double rating, int directorId, string expectedErrorMessage)
        {
            // Arrange
            // Simple model for create
            var movieForCreate = new Movie
            {
                Title = title,
                Release = new DateTime(releaseYear, 1, 1),
                Income = income,
                Rating = rating,
                DirectorId = directorId
            };
            int movieIdBeforeCreate = movieForCreate.Id;

            directorRepository.Setup(x => x.Exists(It.IsNotIn(GetTheoreticalDirectorIds()))).Returns(false);
            directorRepository.Setup(x => x.Exists(It.IsIn(GetTheoreticalDirectorIds()))).Returns(true);

            // Assert - Act
            var exception = Assert.Throws<ArgumentException>(() => service.Create(movieForCreate));

            Assert.That(exception.Message, Is.EqualTo(expectedErrorMessage));
            Assert.That(movieForCreate.Id, Is.EqualTo(movieIdBeforeCreate));
            movieRepository.Verify(m => m.Create(It.IsAny<Movie>()), Times.Never);
            movieRepository.VerifyNoOtherCalls();
            roleRepository.VerifyNoOtherCalls();

            if (title == "director fail")
            {
                directorRepository.Verify(x => x.Exists(movieForCreate.DirectorId), Times.Once);
            }
            else
            {
                directorRepository.Verify(x => x.Exists(It.IsAny<int>()), Times.Never);
            }
        }

        [TestCase("a", null, null, null, null)]
        [TestCase(null, 2026, 1000, null, null)]
        public void UpdateWithSuccessTest(string? title, int? releaseYear, double? income, double? rating, int? directorId)
        {
            // Arrange
            // Simple model for create
            Random random = new Random();

            var createdMovie = new Movie
            {
                Id = random.Next(1, 1000),
                Title = "Test title on create",
                Release = DateTime.Now,
                Income = random.Next(10001),
                Rating = random.Next(1, 11),
                DirectorId = 1
            };

            // Change if the parameters are not null, otherwise keep the original values
            var movieForUpdate = new Movie
            {
                Id = createdMovie.Id,
                Title = title ?? createdMovie.Title,
                Release = releaseYear != null ? new DateTime(releaseYear.Value, 1, 1) : createdMovie.Release,
                Income = income ?? createdMovie.Income,
                Rating = rating ?? createdMovie.Rating,
                DirectorId = directorId ?? createdMovie.DirectorId
            };

            // Imitate repo behaviour
            var nextId = new Random().Next(1, 1000);
            movieRepository.Setup(m => m.Update(It.IsAny<Movie>())).Returns<Movie>((x) => x);
            movieRepository.Setup(m => m.GetById(createdMovie.Id)).Returns(createdMovie);
            directorRepository.Setup(x => x.Exists(It.IsNotIn(GetTheoreticalDirectorIds()))).Returns(false);
            directorRepository.Setup(x => x.Exists(It.IsIn(GetTheoreticalDirectorIds()))).Returns(true);

            // Act
            service.UpdateMovie(movieForUpdate); // This will call the repository's Update method

            // Assert
            Assert.That(movieForUpdate.Id, Is.EqualTo(createdMovie.Id));
            Assert.That(movieForUpdate.Title, Is.EqualTo(title ?? createdMovie.Title));
            Assert.That(movieForUpdate.Release, Is.EqualTo(releaseYear != null ? new DateTime(releaseYear.Value, 1, 1) : createdMovie.Release));
            Assert.That(movieForUpdate.Income, Is.EqualTo(income ?? createdMovie.Income));
            Assert.That(movieForUpdate.Rating, Is.EqualTo(rating ?? createdMovie.Rating));
            Assert.That(movieForUpdate.DirectorId, Is.EqualTo(directorId ?? createdMovie.DirectorId));

            movieRepository.Verify(m => m.Update(It.IsAny<Movie>()), Times.Once);
            movieRepository.Verify(m => m.GetById(createdMovie.Id), Times.Once);
            movieRepository.VerifyNoOtherCalls();
            roleRepository.VerifyNoOtherCalls();
            directorRepository.Verify(x => x.Exists(movieForUpdate.DirectorId), Times.Once);
            directorRepository.VerifyNoOtherCalls();
        }

        [TestCase("", null, null, null, null, "Movie title cannot be empty.")]
        [TestCase("   ", null, null, null, null, "Movie title cannot be empty.")]
        [TestCase("aasdf as fasf asf asf asf dasfd asdf asdf asdf asdf asdf asdf asdf asdf as fdasdf safd sad fsadf sa fdaasdf as fasf asf asf asf dasfd asdf asdf asdf asdf asdf asdf asdf asdf as fdasdf safd sad fsadf sa fdsdfgsgfdsgsdfgsdgertwertweertret123456789012345678901234567890", null, null, null, null, "Movie title cannot be longer than 256 characters.")]
        [TestCase(null, null, null, null, null, "Movie title cannot be empty.")]
        [TestCase("year fail", 200, null, null, null, "Movie release year must be greater than 1888.")]
        [TestCase("income fail", null, -1, null, null, "Movie income must be between 0 and 10000.")]
        [TestCase("income fail", null, 10001, null, null, "Movie income must be between 0 and 10000.")]
        [TestCase("rating fail", null, null, -1, null, "Movie rating must be between 1 and 10.")]
        [TestCase("rating fail", null, null, 13, null, "Movie rating must be between 1 and 10.")]
        [TestCase("director fail", null, null, null, 0, "Director does not exist with id: 0.")]
        public void UpdateWithExceptionTest(string? title, int? releaseYear, double? income, double? rating, int? directorId, string expectedErrorMessage)
        {
            // Arrange
            // Simple model for create
            Random random = new Random();

            var createdMovie = new Movie
            {
                Id = random.Next(1, 1000),
                Title = "Test title on create",
                Release = DateTime.Now,
                Income = random.Next(10001),
                Rating = random.Next(1, 11),
                DirectorId = 1
            };

            // Change if the parameters are not null, otherwise keep the original values
            // note: if title is null and all other parameters are null we eant to change null
            var movieForUpdate = new Movie
            {
                Id = createdMovie.Id,
                Title = title != null || title == null && releaseYear == null && income == null && rating == null && directorId == null
                    ? title
                    : createdMovie.Title,
                Release = releaseYear != null ? new DateTime(releaseYear.Value, 1, 1) : createdMovie.Release,
                Income = income ?? createdMovie.Income,
                Rating = rating ?? createdMovie.Rating,
                DirectorId = directorId ?? createdMovie.DirectorId
            };

            // Imitate repo behaviour
            var nextId = new Random().Next(1, 1000);
            movieRepository.Setup(m => m.Update(It.IsAny<Movie>())).Returns<Movie>((x) => x);
            movieRepository.Setup(m => m.GetById(createdMovie.Id)).Returns(createdMovie);
            directorRepository.Setup(x => x.Exists(It.IsNotIn(GetTheoreticalDirectorIds()))).Returns(false);
            directorRepository.Setup(x => x.Exists(It.IsIn(GetTheoreticalDirectorIds()))).Returns(true);

            // Assert - Act
            var exception = Assert.Throws<ArgumentException>(() => service.UpdateMovie(movieForUpdate));

            Assert.That(exception.Message, Is.EqualTo(expectedErrorMessage));

            // Assert
            Assert.That(movieForUpdate.Id, Is.EqualTo(createdMovie.Id));
            movieRepository.Verify(m => m.Update(It.IsAny<Movie>()), Times.Never);
            movieRepository.Verify(m => m.GetById(createdMovie.Id), Times.Once);
            movieRepository.VerifyNoOtherCalls();
            roleRepository.VerifyNoOtherCalls();

            if (title == "director fail")
            {
                directorRepository.Verify(x => x.Exists(movieForUpdate.DirectorId), Times.Once);
            }
            else
            {
                directorRepository.Verify(x => x.Exists(movieForUpdate.DirectorId), Times.Never);
            }
        }

        [Test]
        public void DeleteTest()
        {
            // Arrange
            var movieIdToDelete = 1;
            // Possible setup for the repository to ensure the movie exists before deletion
            // Possible setup for insert related roles in the DB

            // Act
            service.Delete(movieIdToDelete);

            // Assert
            // Possible asserts comes here even has any related role in the DB
            movieRepository.Verify(m => m.Delete(movieIdToDelete), Times.Once);
            movieRepository.VerifyNoOtherCalls();
            roleRepository.VerifyNoOtherCalls();
            directorRepository.VerifyNoOtherCalls();
        }

        [TestCaseSource(nameof(ComplexDataForFilmsWithDirector))]
        public void GetFilmsWithDirectorDataTest(List<Director> directors, List<Movie> movies)
        {
            // Arrange
            directorRepository.Setup(x => x.GetAll()).Returns(directors.AsQueryable());
            movieRepository.Setup(x => x.GetAll()).Returns(movies.AsQueryable());

            // Act
            var result = service.GetFilmsWithDirectorData();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(movies.Count));

            foreach (var item in result)
            {
                Assert.That(movies.Any(x => x.Title == item.Title));
                Assert.That(item.DirectorName, Is.Not.Null);
            }
        }

        [TestCaseSource(nameof(ComplexDataForFilmsWithDirectorFilteredTest))]
        public void GetFilmsWithDirectorDataFilteredValueTest(List<Director> directors, List<Movie> movies, Predicate<MovieWithDirector> filter, List<MovieWithDirector> expextedResult)
        {
            // Arrange
            directorRepository.Setup(x => x.GetAll()).Returns(directors.AsQueryable());
            movieRepository.Setup(x => x.GetAll()).Returns(movies.AsQueryable());

            // Act
            var result = service.GetFilmsWithDirectorData(filter);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(expextedResult.Count));

            foreach (var item in result)
            {
                Assert.That(expextedResult.Any(x => x.Title == item.Title));
                Assert.That(item.DirectorName, Is.Not.Null);
                Assert.That(filter(item), Is.True);
            }
        }

        [TestCaseSource(nameof(ComplexDataMoviesWithRoles))]
        public void GetMoviesWithRolesTest(List<Director> directors, List<Movie> movies, List<Role> roles, List<(Movie, int)> expectedResult)
        {
            // Arrange
            directorRepository.Setup(x => x.GetAll()).Returns(directors.AsQueryable());
            movieRepository.Setup(x => x.GetAll()).Returns(movies.AsQueryable());
            roleRepository.Setup(x => x.GetAll()).Returns(roles.AsQueryable());

            // Act
            var result = service.GetMoviesWithRoles();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(expectedResult.Count));

            foreach (var item in result)
            {
                var relatedExpectedMovie = expectedResult.SingleOrDefault(x => x.Item1.Id == item.Id);
                Assert.That(relatedExpectedMovie, Is.Not.Null);
                Assert.That(item.RolesCount, Is.EqualTo(relatedExpectedMovie.Item2));
                Assert.That(item.DirectorName, Is.Not.Null); // Lazy load will fail if we did not load directly by hand the data in test data
            }
        }

        public static TestCaseData[] ComplexDataForFilmsWithDirector()
        {
            return [
                new TestCaseData(new List<Director>(), new List<Movie>()), // EmptyDb
                new TestCaseData(new List<Director>()
                {
                    new Director(1, "Bob"),
                    new Director(2, "Robert"),
                    new Director(3, "Dave"),
                },
                new List<Movie>()
                {
                    new Movie(1, "Movie1", 100, 1, new DateTime(2026, 1, 2), 3),
                    new Movie(2, "Movie2", 101, 1, new DateTime(2026, 1, 3), 4),
                    new Movie(3, "Movie3", 102, 1, new DateTime(2026, 1, 4), 5),
                    new Movie(4, "Movie4", 103, 2, new DateTime(2026, 1, 5), 6),
                })
            ];
        }

        public static TestCaseData[] ComplexDataForFilmsWithDirectorFilteredTest()
        {
            Predicate<MovieWithDirector> predicate = (MovieWithDirector m) => m.Income >= 102;

            return [
                new TestCaseData(new List<Director>(), new List<Movie>(), predicate, new List<MovieWithDirector>()), // EmptyDb
                new TestCaseData(new List<Director>()
                {
                    new Director(1, "Bob"),
                    new Director(2, "Robert"),
                    new Director(3, "Dave"),
                },
                new List<Movie>()
                {
                    new Movie(1, "Movie1", 100, 1, new DateTime(2026, 1, 2), 3),
                    new Movie(2, "Movie2", 101, 1, new DateTime(2026, 1, 3), 4),
                    new Movie(3, "Movie3", 102, 1, new DateTime(2026, 1, 4), 5),
                    new Movie(4, "Movie4", 103, 2, new DateTime(2026, 1, 5), 6),
                },
                predicate,
                new List<MovieWithDirector>()
                {
                    new MovieWithDirector()
                    {
                        Id = 3,
                        Title = "Movie3",
                        Income = 102,
                        DirectorId = 1,
                        Release = new DateTime(2026, 1, 4),
                        Rating = 5,
                        DirectorName = "Bob"
                    },
                    new MovieWithDirector()
                    {
                        Id = 4,
                        Title = "Movie4",
                        Income = 103,
                        DirectorId = 2,
                        Release = new DateTime(2026, 1, 5),
                        Rating = 6,
                        DirectorName = "Dave"
                    },
                }),
            ];
        }

        public static TestCaseData[] ComplexDataMoviesWithRoles()
        {
            var directors = new List<Director>()
                {
                    new Director(1, "Bob"),
                    new Director(2, "Robert"),
                    new Director(3, "Dave"),
                };

            var movies = new List<Movie>()
                {
                    new Movie(1, "Movie1", 100, 1, new DateTime(2026, 1, 2), 3),
                    new Movie(2, "Movie2", 101, 1, new DateTime(2026, 1, 3), 4),
                    new Movie(3, "Movie3", 102, 1, new DateTime(2026, 1, 4), 5),
                    new Movie(4, "Movie4", 103, 2, new DateTime(2026, 1, 5), 6),
                };

            // For LazyLoading/EagerLoading imitation
            movies[0].Director = directors[0];
            movies[1].Director = directors[0];
            movies[2].Director = directors[0];
            movies[3].Director = directors[1];

            return [
                new TestCaseData(new List<Director>(), new List<Movie>(), new List<Role>(), new List<(Movie, int)>()), // EmptyDb
                new TestCaseData(directors, movies,
                new List<Role>()
                {
                    new Role(1, 1, 1, 3, "Albert the magician"),
                    new Role(2, 1, 2, 2, "Didi the dragon"),
                    new Role(3, 2, 3, 1, "Mr. Smith1"),
                    new Role(4, 2, 3, 1, "Mr. Smith2"),
                    new Role(5, 2, 3, 1, "Mr. Smith3"),
                    new Role(6, 2, 3, 1, "Mr. Smith4"),
                    new Role(7, 2, 3, 1, "Mr. Smith5"),
                    new Role(8, 2, 3, 1, "Mr. Smith6"),
                    new Role(9, 2, 3, 1, "Mr. Smith7"),
                    new Role(10, 2, 3, 1, "Mr. Smith8"),
                    new Role(11, 2, 3, 1, "Mr. Smith9"),
                    new Role(12, 2, 4, 1, "Neo"),
                    new Role(13, 2, 5, 1, "Trinity"),
                    new Role(14, 2, 6, 1, "Morpheus"),
                    new Role(15, 2, 7, 5, "Second pilot"),
                    new Role(16, 2, 8, 5, "Third pilot"),
                    new Role(17, 2, 9, 5, "Fourth pilot"),
                }, new List<(Movie, int)>() // The expected result
                {
                    (new Movie(2, "Movie2", 101, 1, new DateTime(2026, 1, 3), 4), 15),
                }),
            ];
        }
    }
}
