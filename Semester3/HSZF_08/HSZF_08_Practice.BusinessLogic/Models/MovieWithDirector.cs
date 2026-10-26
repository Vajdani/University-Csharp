namespace HSZF_08_Practice.BusinessLogic.Models
{
    public class MovieWithDirector
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DirectorId { get; set; }
        public string DirectorName { get; set; }
        public double Rating { get; set; }
        public double Income { get; set; }
        public DateTime Release { get; set; }
    }
}
