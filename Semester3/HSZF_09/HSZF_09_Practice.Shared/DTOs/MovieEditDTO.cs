namespace HSZF_09_Practice.Shared.DTOs
{
    public class MovieEditDTO : MovieCreateDTO
    {
        public int Id { get; set; }
        
        public static MovieEditDTO CreateFrom(MovieCreateDTO dto)
        {
            return new MovieEditDTO()
            {
                DirectorId = dto.DirectorId,
                Income = dto.Income,
                Rating = dto.Rating,
                Release = dto.Release,
                Title = dto.Title,
            };
        }
    }
}
