using System.ComponentModel.DataAnnotations;

namespace Calories.API.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }
        [Required]
        public string RecipeName { get; set; } = string.Empty;
        //public double RecipeCalories { get; set; }
    }
}