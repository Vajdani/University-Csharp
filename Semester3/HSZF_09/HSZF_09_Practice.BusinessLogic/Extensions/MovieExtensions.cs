using HSZF_09_Practice.BusinessLogic.Models;

namespace HSZF_09_Practice.BusinessLogic.Extensions
{
    public static class MovieExtensions
    {
        public static IEnumerable<MovieWithDirector> CustomFilterBy(this IEnumerable<MovieWithDirector> collection, Predicate<MovieWithDirector> predicate)
        {
            return collection.Where(x => predicate(x));
        }

        public static IQueryable<MovieWithDirector> CustomFilterBy(this IQueryable<MovieWithDirector> collection, Predicate<MovieWithDirector> predicate)
        {
            return collection.Where(x => predicate(x));
        }
    }
}
