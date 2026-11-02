namespace HSZF_09_Practice.Shared.DTOs
{
    public class RoleEditDTO : RoleCreateDTO
    {
        public int Id { get; set; }

        public static RoleEditDTO CreateFrom(RoleCreateDTO dto)
        {
            return new RoleEditDTO()
            {
                ActorId = dto.ActorId,
                MovieId = dto.MovieId,
                Name = dto.Name,
                Priority = dto.Priority,
            };
        }
    }
}
