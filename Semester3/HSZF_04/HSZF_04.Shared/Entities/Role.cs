using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HSZF_04.Shared.Entities
{
    [Table("Roles")]
    public class Role
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // CompositKey example
        //[Key, Column(Order = 0)] // Not necessary the Order definition, use the same prop ordering
        [ForeignKey(nameof(Role.Movie))]
        public int MovieId { get; set; }

        // CompositKey example
        //[Key, Column(Order = 1)] // Not necessary the Order definition, use the same prop ordering
        [ForeignKey(nameof(Role.Actor))]
        public int ActorId { get; set; }

        [Range(1, 10)]
        public int Priority { get; set; }

        [StringLength(512)]
        public string Name { get; set; }

        #region Navigation props

        [NotMapped]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public virtual Movie Movie { get; set; }

        [NotMapped]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public virtual Actor Actor { get; set; }

        #endregion

        // Necessary for EF auto fill
        public Role()
        {
        }

        // For seed
        public Role(int id, int movieId, int actorId, int priority, string name) : this()
        {
            Id = id;
            MovieId = movieId;
            ActorId = actorId;
            Priority = priority;
            Name = name;
        }
    }
}
