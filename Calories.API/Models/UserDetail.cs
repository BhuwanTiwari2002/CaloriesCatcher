using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Calories.API.Models
{
    public class UserDetail
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; } = string.Empty;
        public double Height { get; set; }
        public double Weight { get; set; }
        public int DailyCalories { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        [ForeignKey("CalorieId")]
        public int CalorieId { get; set; }
        [ForeignKey("RecipeFoodId")]
        public int RecipeFoodId { get; set; }
    }
}
