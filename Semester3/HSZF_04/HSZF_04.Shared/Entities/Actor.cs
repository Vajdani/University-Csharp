using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HSZF_04.Shared.Entities
{
    [Table("Actors")]
    public class Actor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        #region Navigation props

        [NotMapped]
        [ForeignKey(nameof(Role.ActorId))]
        public virtual ICollection<Role> Roles { get; set; }

        #endregion

        public Actor()
        {
            Roles = new HashSet<Role>();
        }

        public Actor(int id, string name) : this()
        {
            Id = id;
            Name = name;
        }
    }
}
