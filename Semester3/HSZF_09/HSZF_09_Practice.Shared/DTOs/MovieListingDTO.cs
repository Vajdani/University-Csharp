namespace HSZF_09_Practice.Shared.DTOs
{
    public class MovieListingDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Income { get; set; }
        public double Rating { get; set; }
        public DateTime Release { get; set; }
        public int DirectorId { get; set; }
        public string DirectorName { get; set; } // For example custom data

        public MovieListingDTO()
        {
        }

        public MovieListingDTO(int id, string title, double income, double rating, DateTime release, int directorId, string directorName)
        {
            Id = id;
            Title = title;
            Income = income;
            Rating = rating;
            Release = release;
            DirectorId = directorId;
            DirectorName = directorName;
        }
    }
}
