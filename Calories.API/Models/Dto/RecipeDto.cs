namespace Calories.API.Models.Dto
{
    public class RecipeDto
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set;} = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public double Calories { get; set; }
        public string ImagePath { get; set; } = string.Empty;
    }
}
