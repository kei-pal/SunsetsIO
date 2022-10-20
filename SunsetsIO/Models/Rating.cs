using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SunsetsIO.Models
{
    public class Rating
    {
        public int Id { get; set; }
        [Range(1,5)]
        public int Stars { get; set; }
        [ForeignKey("User")]
        [MaxLength(450)]
        public string? UserId { get; set; }
        
        // TODO: figure out why below only works if declared nullable
        public User? User { get; set; }
        // TODO: add location of rating
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime DateTimeRated { get; set; }
    }
}
