namespace HSZF_09_Practice.Shared.DTOs
{
    public class MovieCommonDataDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Income { get; set; }
        public double Rating { get; set; }
        public DateTime Release { get; set; }
        public int DirectorId { get; set; }

        public MovieCommonDataDTO()
        {
        }

        public MovieCommonDataDTO(int id, string title, double income, double rating, DateTime release, int directorId)
        {
            Id = id;
            Title = title;
            Income = income;
            Rating = rating;
            Release = release;
            DirectorId = directorId;
        }
    }
}
