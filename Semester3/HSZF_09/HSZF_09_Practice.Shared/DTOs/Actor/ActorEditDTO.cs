namespace HSZF_09_Practice.Shared.DTOs.Actor
{
    public class ActorEditDTO : ActorCreateDTO
    {
        public int Id { get; set; }

        public static ActorEditDTO CreateFrom(ActorCreateDTO dto)
        {
            return new ActorEditDTO
            {
                Name = dto.Name,
            };
        }
    }
}
