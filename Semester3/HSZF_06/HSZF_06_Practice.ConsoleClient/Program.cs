using ConsoleTools;
using HSZF_06_Practice.BusinessLogic.Interfaces;
using HSZF_06_Practice.BusinessLogic.Services;
using HSZF_06_Practice.DataAccess.Contexts;
using HSZF_06_Practice.DataAccess.Repositories;
using HSZF_06_Practice.Shared.Entities;

namespace HSZF_06_Practice.ConsoleClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Movie DB handler app");

            var context = new MovieDBContext();
            var directorRepository = new DirectorRepository(context);
            var actorRepository = new ActorRepository(context);
            var roleRepository = new RoleRepository(context);
            var movieRepository = new MovieRepository(context);

            var personService = new PersonService(directorRepository, actorRepository, roleRepository);
            var movieService = new MovieService(movieRepository, roleRepository, directorRepository);
            var roleService = new RoleService(roleRepository, actorRepository, movieRepository);

            ConsoleMenu consoleMenu = GetMenu(personService, movieService, roleService);

            consoleMenu.Show();
        }

        private static ConsoleMenu GetMenu(PersonService personService, MovieService movieService, RoleService roleService)
        {
            var consoleMenu = new ConsoleMenu();

            consoleMenu
                .Add("Director", GetDirectorSubMenu(personService).Show)
                // TODO: Add similar menu items for Actor, Movie, and Role management
                .Add("Query #1: Films with Director Data", () =>
                {
                    FilmsWithDirectorData(movieService);

                    Console.ReadLine();
                })
                .Add("Query #2: Roles by actors", () =>
                {
                    RolesByActors(roleService);

                    Console.ReadLine();
                })
                .Add("Query #2: Roles by actors with eager load", () =>
                {
                    RolesByActorsWithEagerLoad(roleService);

                    Console.ReadLine();
                })
                .Add("Query #3: Movies with roles", () =>
                {
                    MoviesWithRoles(movieService);

                    Console.ReadLine();
                })
                .Add("Query #4: Director stats", () =>
                {
                    DirectorStat(personService);

                    Console.ReadLine();
                })
                .Add("Query #5: Actor pairs in same movies", () =>
                {
                    ActorPairsInSameMovies(personService);

                    Console.ReadLine();
                })
                .Add("Quit", () =>
                {
                    Environment.Exit(0);
                });
            return consoleMenu;
        }

        private static ConsoleMenu GetDirectorSubMenu(IPersonService personService)
        {
            return new ConsoleMenu([], 1)
            .Add("List all", () =>
            {
                foreach (var director in personService.GetAllDirectors())
                {
                    Console.WriteLine($"Id: {director.Id}, Name: {director.Name}");
                }

                Console.ReadLine();
            })
            .Add("Create", () =>
            {
                Console.Write("Enter director name: ");
                var name = Console.ReadLine();

                var director = new Director
                {
                    Name = name
                };
                personService.CreateDirector(director);

                Console.WriteLine($"Director created with Id: {director.Id}");

                Console.ReadLine();
            })
            .Add("Update", () =>
            {
                Console.Write("Enter director Id to update: ");
                var id = int.Parse(Console.ReadLine());

                var existingDirector = personService.GetDirectorById(id);

                if (existingDirector == null)
                {
                    Console.WriteLine("Director not found.");
                    return;
                }

                Console.Write($"Enter new name for director (current: {existingDirector.Name}): ");
                var newName = Console.ReadLine();

                existingDirector.Name = newName;

                personService.UpdateDirector(existingDirector);

                Console.WriteLine("Director updated successfully.");

                Console.ReadLine();
            })
            .Add("Delete", () =>
            {
                Console.Write("Enter director Id to delete: ");
                var id = int.Parse(Console.ReadLine());

                personService.DeleteDirector(id);

                Console.WriteLine("Director deleted successfully.");
            })
            .Add("Back to main menu", ConsoleMenu.Close);
        }
        private static void ActorPairsInSameMovies(IPersonService personService)
        {
            // Query5
            Console.WriteLine("--- Query #5: Actor pairs in same movies ---");

            var actorPairs = personService.GetActorPairsInSameMovies();

            foreach (var actor in actorPairs)
            {
                Console.WriteLine(actor);
            }
        }

        private static void DirectorStat(IPersonService personService)
        {
            //Query 4
            Console.WriteLine("--- Query #4: DirectorStats ---");

            var directors = personService.GetDirectorStat();

            foreach (var director in directors)
            {
                Console.WriteLine("{0} | Number of movies: {1} | AvgRating: {2} | Total Inc.: {3} | Highest Inc.: {4}", director.DirectorName, director.NumberOfMovies, director.AverageRating, director.TotalIncome, director.HighestIncome);
            }
        }

        private static void MoviesWithRoles(IMovieService movieService)
        {
            // Query 3
            Console.WriteLine("---Query #3: Movies with role numbers over 14 roles  ---");

            var moviesWithRoleNums = movieService.GetMoviesWithRoles();

            foreach (var movie in moviesWithRoleNums)
            {
                Console.WriteLine("{0} ({1}) Rating: {2} - Role numbers: {3}", movie.Title, movie.DirectorName, movie.Rating, movie.RolesCount);
            }
        }

        private static void RolesByActors(IRoleService roleService)
        {
            // Query #2
            Console.WriteLine("--- Query #2: Actors with their roles in movies up to role priority 5 --- ");
            var actorsWithRoles = roleService.GetRolesByActors();

            foreach (var actor in actorsWithRoles)
            {
                Console.WriteLine("{0} | {1} | {2} | {3}", actor.ActorName, actor.MovieTitle, actor.RoleName, actor.Priority);
            }
        }

        private static void RolesByActorsWithEagerLoad(IRoleService roleService)
        {
            // Query #2
            Console.WriteLine("--- Query #2: Actors with their roles in movies up to role priority 5 --- ");
            Console.WriteLine("\nWith navprops and eager load:");

            var actorsWithRoles2 = roleService.GetRolesByActorsWithEagerLoad();

            foreach (var actor in actorsWithRoles2)
            {
                Console.WriteLine("{0} | {1} | {2} | {3}", actor.ActorName, actor.MovieTitle, actor.RoleName, actor.Priority);
            }
        }

        private static void FilmsWithDirectorData(IMovieService movieService)
        {
            // Query #1.
            Console.WriteLine("--- Query #1: Films with director data --- ");
            var movieDetails = movieService.GetFilmsWithDirectorData();

            foreach (var movie in movieDetails)
            {
                Console.WriteLine("[{0}] {1} (Director: {2}) - Rating: {3}; Income: {4}", movie.Release.Year, movie.Title, movie.DirectorName, movie.Rating, movie.Income);
            }
        }
    }
}
