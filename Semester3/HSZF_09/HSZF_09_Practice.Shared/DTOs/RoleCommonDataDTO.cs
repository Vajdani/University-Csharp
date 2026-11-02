namespace HSZF_09_Practice.Shared.DTOs
{
    public class RoleCommonDataDTO
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        public int Priority { get; set; }
        public string Name { get; set; }

        public RoleCommonDataDTO()
        {
        }

        public RoleCommonDataDTO(int id, int movieId, int actorId, int priority, string name)
        {
            Id = id;
            MovieId = movieId;
            ActorId = actorId;
            Priority = priority;
            Name = name;
        }
    }
}
