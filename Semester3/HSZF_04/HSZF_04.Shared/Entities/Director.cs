using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HSZF_04.Shared.Entities
{
    [Table("Directors")]
    public class Director
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(240)]
        public string Name { get; set; }

        #region Navigation props

        [NotMapped]
        [ForeignKey(nameof(Movie.DirectorId))]
        public virtual ICollection<Movie> Movies { get; set; }

        #endregion

        public Director()
        {
            Movies = new HashSet<Movie>();
        }

        public Director(int id, string name) : this()
        {
            Id = id;
            Name = name;
        }
    }
}
