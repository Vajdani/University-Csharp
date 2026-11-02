namespace HSZF_09_Practice.Shared.DTOs
{
    public class RoleListingDTO
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public int ActorId { get; set; }
        public string ActorName { get; set; }
        public string RoleName { get; set; }
        public int Priority { get; set; }

        public RoleListingDTO()
        {
        }

        public RoleListingDTO(int id, int movieId, string movieTitle, int actorId, string actorName, string roleName, int priority)
        {
            Id = id;
            MovieId = movieId;
            MovieTitle = movieTitle;
            ActorId = actorId;
            ActorName = actorName;
            RoleName = roleName;
            Priority = priority;
        }
    }
}
