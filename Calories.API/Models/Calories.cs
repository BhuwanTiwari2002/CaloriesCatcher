using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calories.API.Models
{
    public class Calories
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(450)]
        public virtual string UserId { get; set; } = string.Empty;
        [Required]
        [Column(TypeName = "DECIMAL")]
        public double Calorie { get; set; }
        [Required]
        public string CalorieName { get; set; } = string.Empty;
        [Required]
        public DateTime Date { get; set; }
    }
}
