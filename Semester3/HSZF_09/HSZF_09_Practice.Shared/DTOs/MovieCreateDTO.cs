namespace HSZF_09_Practice.Shared.DTOs
{
    public class MovieCreateDTO
    {
        public string Title { get; set; }
        public double Income { get; set; }
        public double Rating { get; set; }
        public DateTime Release { get; set; }
        public int DirectorId { get; set; }
    }
}
