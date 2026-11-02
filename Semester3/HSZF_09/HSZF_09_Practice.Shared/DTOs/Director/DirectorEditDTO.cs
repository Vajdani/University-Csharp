namespace HSZF_09_Practice.Shared.DTOs.Director
{
    public class DirectorEditDTO : DirectorCreateDTO
    {
        public int Id { get; set; }

        public static DirectorEditDTO CreateFrom(DirectorCreateDTO dto)
        {
            return new DirectorEditDTO
            {
                Name = dto.Name,
            };
        }
    }
}
