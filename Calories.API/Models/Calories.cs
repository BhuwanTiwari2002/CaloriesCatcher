using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calories.API.Models
{
    public class Calories
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; } = string.Empty;
        [Required]
        public double Calorie { get; set; }
        [Required]
        public string CalorieName { get; set; } = string.Empty;
        [Required]
        public DateTime Date { get; set; }
    }
}
