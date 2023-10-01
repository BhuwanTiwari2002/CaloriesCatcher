using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calories.API.Models
{
    public class RecipeFood
    {
        [Key]
        public int RecipeFoodId { get; set; }
        [ForeignKey("FoodId")]
        public int FoodId { get; set; }
        [ForeignKey("RecipeId")]
        public int RecipeId { get; set; }
        [ForeignKey("UserId")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(450)]
        public string UserId { get; set; } = string.Empty;
    }
}
