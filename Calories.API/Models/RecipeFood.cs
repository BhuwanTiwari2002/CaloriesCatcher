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
        public string UserId { get; set; } = string.Empty;
    }
}
