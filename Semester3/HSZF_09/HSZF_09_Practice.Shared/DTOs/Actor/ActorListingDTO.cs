namespace HSZF_09_Practice.Shared.DTOs.Actor
{
    public class ActorListingDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ActorListingDTO()
        {
        }

        public ActorListingDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
