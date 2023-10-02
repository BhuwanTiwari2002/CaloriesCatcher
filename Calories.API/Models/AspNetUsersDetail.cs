using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Calories.API.Models
{
    public class AspNetUsersDetail
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(450)]
        public string UserId { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        [Column(TypeName = "DECIMAL")]
        public int DailyCalories { get; set; }
        public DateTime BirthDate { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Gender { get; set; } = string.Empty;
        [ForeignKey("CalorieId")]
        public int CalorieId { get; set; }
        [ForeignKey("RecipeFoodId")]
        public int RecipeFoodId { get; set; }
    }
}
