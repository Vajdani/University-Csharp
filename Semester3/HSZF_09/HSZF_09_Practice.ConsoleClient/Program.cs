using ConsoleTools;
using HSZF_09_Practice.ConsoleClient.HTTPClient;
using HSZF_09_Practice.Shared.DTOs;
using HSZF_09_Practice.Shared.DTOs.Actor;
using HSZF_09_Practice.Shared.DTOs.Director;

namespace HSZF_09_Practice.ConsoleClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Movie DB handler app");

            var client = new Client("http://localhost:3113", new HttpClient());
            client.JsonSerializerSettings.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;

            ConsoleMenu consoleMenu = GetMenu(client);

            consoleMenu.Show();
        }

        private static ConsoleMenu GetMenu(Client client)
        {
            var consoleMenu = new ConsoleMenu();

            consoleMenu
                .Add("Director", GetDirectorSubMenu(client).Show)
                .Add("Actor", GetActorSubMenu(client).Show)
                .Add("Movie", GetMovieSubMenu(client).Show)
                .Add("Role", GetRoleSubMenu(client).Show)
                .Add("Query #1: Films with Director Data", () =>
                {
                    FilmsWithDirectorData(client);

                    Console.ReadLine();
                })
                .Add("Query #2: Roles by actors", () =>
                {
                    RolesByActors(client);

                    Console.ReadLine();
                })
                .Add("Query #3: Movies with roles", () =>
                {
                    MoviesWithRoles(client);

                    Console.ReadLine();
                })
                .Add("Query #4: Director stats", () =>
                {
                    DirectorStat(client);

                    Console.ReadLine();
                })
                .Add("Query #5: Actor pairs in same movies", () =>
                {
                    ActorPairsInSameMovies(client);

                    Console.ReadLine();
                })
                .Add("Quit", () =>
                {
                    Environment.Exit(0);
                });
            return consoleMenu;
        }

        private static ConsoleMenu GetDirectorSubMenu(Client client)
        {
            return new ConsoleMenu([], 1)
            .Add("List all", () =>
            {
                foreach (var director in client.PersonListDirectors())
                {
                    Console.WriteLine($"Id: {director.Id}, Name: {director.Name}");
                }

                Console.ReadLine();
            })
            .Add("Create", () =>
            {
                Console.Write("Enter director name: ");
                var name = Console.ReadLine();

                var director = new DirectorCreateDTO
                {
                    Name = name
                };

                client.PersonCreateDirector(director);

                Console.WriteLine($"Director created.");

                Console.ReadLine();
            })
            .Add("Update", () =>
            {
                Console.Write("Enter director Id to update: ");
                var id = int.Parse(Console.ReadLine());

                var existingDirector = client.PersonGetDirector(id);

                if (existingDirector == null)
                {
                    Console.WriteLine("Director not found.");
                    return;
                }

                Console.Write($"Enter new name for director (current: {existingDirector.Name}): ");
                var newName = Console.ReadLine();

                var model = new DirectorEditDTO()
                {
                    Id = existingDirector.Id,
                    Name = newName
                };

                client.PersonUpdateDirector(model);

                Console.WriteLine("Director updated successfully.");

                Console.ReadLine();
            })
            .Add("Delete", () =>
            {
                Console.Write("Enter director Id to delete: ");
                var id = int.Parse(Console.ReadLine());

                client.PersonDeleteDirector(id);

                Console.WriteLine("Director deleted successfully.");
            })
            .Add("Back to main menu", ConsoleMenu.Close);
        }

        private static ConsoleMenu GetActorSubMenu(Client client)
        {
            return new ConsoleMenu([], 1)
            .Add("List all", () =>
            {
                foreach (var actor in client.PersonListActors())
                {
                    Console.WriteLine($"Id: {actor.Id}, Name: {actor.Name}");
                }

                Console.ReadLine();
            })
            .Add("Create", () =>
            {
                Console.Write("Enter actor name: ");
                var name = Console.ReadLine();

                var actor = new ActorCreateDTO
                {
                    Name = name
                };

                client.PersonCreateActor(actor);

                Console.WriteLine($"Actor created.");

                Console.ReadLine();
            })
            .Add("Update", () =>
            {
                Console.Write("Enter actor Id to update: ");
                var id = int.Parse(Console.ReadLine());

                var existingActor = client.PersonGetActor(id);

                if (existingActor == null)
                {
                    Console.WriteLine("Actor not found.");
                    return;
                }

                Console.Write($"Enter new name for actor (current: {existingActor.Name}): ");
                var newName = Console.ReadLine();

                var model = new ActorEditDTO()
                {
                    Id = existingActor.Id,
                    Name = newName
                };

                client.PersonUpdateActor(model);

                Console.WriteLine("Actor updated successfully.");

                Console.ReadLine();
            })
            .Add("Delete", () =>
            {
                Console.Write("Enter actor Id to delete: ");
                var id = int.Parse(Console.ReadLine());

                client.PersonDeleteActor(id);

                Console.WriteLine("Actor deleted successfully.");
            })
            .Add("Back to main menu", ConsoleMenu.Close);
        }

        private static ConsoleMenu GetMovieSubMenu(Client client)
        {
            return new ConsoleMenu([], 1)
            .Add("List all", () =>
            {
                foreach (var movie in client.MovieList())
                {
                    Console.WriteLine($"Id: {movie.Id}, Title: {movie.Title}, Released: {movie.Release.Year}, Income: {movie.Income}, Rating: {movie.Rating}, DirectorId: {movie.DirectorId}, DirectorName: {movie.DirectorName}");
                }

                Console.ReadLine();
            })
            .Add("Create", () =>
            {
                Console.Write("Enter movie title: ");
                var title = Console.ReadLine();

                Console.Write("Enter movie release year: ");
                var year = int.Parse(Console.ReadLine());

                Console.Write("Enter movie income: ");
                var income = double.Parse(Console.ReadLine());

                Console.Write("Enter movie rating: ");
                var rating = double.Parse(Console.ReadLine());

                DirectorListingDTO director;

                do
                {

                    Console.Write("Enter movie director name: ");
                    var directorName = Console.ReadLine();

                    // TODO: create dedicated endpoint
                    director = client.PersonListDirectors().FirstOrDefault(x => x.Name.ToLower().Contains(directorName.ToLower()));
                } while (director == null);

                var movie = new MovieCreateDTO
                {
                    Title = title,
                    DirectorId = director.Id,
                    Income = income,
                    Rating = rating,
                    Release = new DateTime(year, 1, 1)
                };

                client.MovieCreate(movie);

                Console.WriteLine($"Movie created.");

                Console.ReadLine();
            })
            .Add("Update", () =>
            {
                Console.Write("Enter movie Id to update: ");
                var id = int.Parse(Console.ReadLine());

                var existingMovie = client.MovieGet(id);

                if (existingMovie == null)
                {
                    Console.WriteLine("Movie not found.");
                    return;
                }

                Console.Write("Enter movie title: ");
                var title = Console.ReadLine();

                Console.Write("Enter movie release year: ");
                var year = int.Parse(Console.ReadLine());

                Console.Write("Enter movie income: ");
                var income = double.Parse(Console.ReadLine());

                Console.Write("Enter movie rating: ");
                var rating = double.Parse(Console.ReadLine());

                DirectorListingDTO director;

                do
                {

                    Console.Write("Enter movie director name: ");
                    var directorName = Console.ReadLine();

                    // TODO: create dedicated endpoint
                    director = client.PersonListDirectors().FirstOrDefault(x => x.Name.ToLower().Contains(directorName.ToLower()));
                } while (director == null);

                var model = new MovieEditDTO()
                {
                    Id = existingMovie.Id,
                    Title = title,
                    DirectorId = director.Id,
                    Income = income,
                    Rating = rating,
                    Release = new DateTime(year, 1, 1)
                };

                client.MovieUpdate(model);

                Console.WriteLine("Movie updated successfully.");

                Console.ReadLine();
            })
            .Add("Delete", () =>
            {
                Console.Write("Enter movie Id to delete: ");
                var id = int.Parse(Console.ReadLine());

                client.MovieDelete(id);

                Console.WriteLine("Movie deleted successfully.");
            })
            .Add("Back to main menu", ConsoleMenu.Close);
        }

        private static ConsoleMenu GetRoleSubMenu(Client client)
        {
            return new ConsoleMenu([], 1)
            .Add("List all", () =>
            {
                foreach (var role in client.RoleList())
                {
                    Console.WriteLine($"Id: {role.Id}, MovieTitle: {role.MovieTitle}, ActorName: {role.ActorName}, RoleName: {role.RoleName}, Priority: {role.Priority}");
                }

                Console.ReadLine();
            })
            .Add("Create", () =>
            {
                Console.Write("Enter role name: ");
                var name = Console.ReadLine();

                Console.Write("Enter role priority: ");
                var priority = int.Parse(Console.ReadLine());

                MovieListingDTO movie;

                do
                {

                    Console.Write("Enter movie title: ");
                    var title = Console.ReadLine();

                    // TODO: create dedicated endpoint
                    movie = client.MovieList().FirstOrDefault(x => x.Title.ToLower().Contains(title.ToLower()));
                } while (movie == null);

                ActorListingDTO actor;

                do
                {

                    Console.Write("Enter actor name: ");
                    var actorName = Console.ReadLine();

                    // TODO: create dedicated endpoint
                    actor = client.PersonListActors().FirstOrDefault(x => x.Name.ToLower().Contains(actorName.ToLower()));
                } while (actor == null);

                var role = new RoleCreateDTO
                {
                    Name = name,
                    Priority = priority,
                    ActorId = actor.Id,
                    MovieId = movie.Id,
                };

                client.RoleCreate(role);

                Console.WriteLine($"Role created.");

                Console.ReadLine();
            })
            .Add("Update", () =>
            {
                Console.Write("Enter role Id to update: ");
                var id = int.Parse(Console.ReadLine());

                var existingRole = client.RoleGet(id);

                if (existingRole == null)
                {
                    Console.WriteLine("Role not found.");
                    return;
                }

                Console.Write("Enter role name: ");
                var name = Console.ReadLine();

                Console.Write("Enter role priority: ");
                var priority = int.Parse(Console.ReadLine());

                MovieListingDTO movie;

                do
                {

                    Console.Write("Enter movie title: ");
                    var title = Console.ReadLine();

                    // TODO: create dedicated endpoint
                    movie = client.MovieList().FirstOrDefault(x => x.Title.ToLower().Contains(title.ToLower()));
                } while (movie == null);

                ActorListingDTO actor;

                do
                {

                    Console.Write("Enter actor name: ");
                    var actorName = Console.ReadLine();

                    // TODO: create dedicated endpoint
                    actor = client.PersonListActors().FirstOrDefault(x => x.Name.ToLower().Contains(actorName.ToLower()));
                } while (actor == null);

                var model = new RoleEditDTO()
                {
                    Id = existingRole.Id,
                    Name = name,
                    Priority = priority,
                    ActorId = actor.Id,
                    MovieId = movie.Id,
                };

                client.RoleUpdate(model);

                Console.WriteLine("Role updated successfully.");

                Console.ReadLine();
            })
            .Add("Delete", () =>
            {
                Console.Write("Enter role Id to delete: ");
                var id = int.Parse(Console.ReadLine());

                client.RoleDelete(id);

                Console.WriteLine("Role deleted successfully.");
            })
            .Add("Back to main menu", ConsoleMenu.Close);
        }

        private static void ActorPairsInSameMovies(Client client)
        {
            // Query5
            Console.WriteLine("--- Query #5: Actor pairs in same movies ---");

            var actorPairs = client.PersonGetActorPairsInSameMovies();

            foreach (var actor in actorPairs)
            {
                Console.WriteLine(actor);
            }
        }

        private static void DirectorStat(Client client)
        {
            //Query 4
            Console.WriteLine("--- Query #4: DirectorStats ---");

            var directors = client.PersonGetDirectorStat();

            foreach (var director in directors)
            {
                Console.WriteLine("{0} | Number of movies: {1} | AvgRating: {2} | Total Inc.: {3} | Highest Inc.: {4}", director.DirectorName, director.NumberOfMovies, director.AverageRating, director.TotalIncome, director.HighestIncome);
            }
        }

        private static void MoviesWithRoles(Client client)
        {
            // Query 3
            Console.WriteLine("---Query #3: Movies with role numbers over 14 roles  ---");

            var moviesWithRoleNums = client.MovieGetMoviesWithRoles();

            foreach (var movie in moviesWithRoleNums)
            {
                Console.WriteLine("{0} ({1}) Rating: {2} - Role numbers: {3}", movie.Title, movie.DirectorName, movie.Rating, movie.RolesCount);
            }
        }

        private static void RolesByActors(Client client)
        {
            // Query #2
            Console.WriteLine("--- Query #2: Actors with their roles in movies up to role priority 5 --- ");
            var actorsWithRoles = client.RoleGetRolesWithActorsAndMovieTitles();

            foreach (var actor in actorsWithRoles)
            {
                Console.WriteLine("{0} | {1} | {2} | {3}", actor.ActorName, actor.MovieTitle, actor.RoleName, actor.Priority);
            }
        }

        private static void FilmsWithDirectorData(Client client)
        {
            // Query #1.
            Console.WriteLine("--- Query #1: Films with director data --- ");
            var movieDetails = client.MovieList();

            foreach (var movie in movieDetails)
            {
                Console.WriteLine("[{0}] {1} (Director: {2}) - Rating: {3}; Income: {4}", movie.Release.Year, movie.Title, movie.DirectorName, movie.Rating, movie.Income);
            }
        }
    }
}
