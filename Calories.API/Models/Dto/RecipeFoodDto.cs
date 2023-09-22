namespace Calories.API.Models.Dto
{
    public class RecipeFoodDto
    {
        public int FoodId { get; set; }
        public int RecipeId { get; set; }
        public int RecipeFoodId { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
