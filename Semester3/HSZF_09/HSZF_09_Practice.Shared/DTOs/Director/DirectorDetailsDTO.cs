namespace HSZF_09_Practice.Shared.DTOs.Director
{
    public class DirectorDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MovieCommonDataDTO> Movies { get; set; }
    }
}
