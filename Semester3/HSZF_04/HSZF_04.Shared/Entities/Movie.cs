using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HSZF_04.Shared.Entities
{
    [Table("Movies")]
    public class Movie
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(240)]
        public string Title { get; set; }

        [Range(0, 10000)]
        public double Income { get; set; }

        [Range(0, 10)]
        public double Rating { get; set; }

        public DateTime Release { get; set; }

        //[Required]
        [ForeignKey(nameof(Movie.Director))]
        public int DirectorId { get; set; }

        #region Navigation props

        [NotMapped]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public virtual Director Director { get; set; }

        [NotMapped]
        [ForeignKey(nameof(Role.MovieId))]
        public virtual ICollection<Role> Roles { get; set; }

        #endregion

        public Movie()
        {
            Roles = new HashSet<Role>();
        }

        public Movie(int id, string title, double income, int directorId, DateTime release, double rating) : this()
        {
            Id = id;
            Title = title;
            Income = income;
            Rating = rating;
            Release = release;
            DirectorId = directorId;
        }
    }
}
