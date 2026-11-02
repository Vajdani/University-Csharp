namespace HSZF_09_Practice.Shared.DTOs.Director
{
    public class DirectorListingDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DirectorListingDTO()
        {
        }

        public DirectorListingDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
