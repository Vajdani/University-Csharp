namespace HSZF_09_Practice.Shared.DTOs.Actor
{
    public class ActorDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RoleListingDTO> Roles { get; set; }
    }
}
