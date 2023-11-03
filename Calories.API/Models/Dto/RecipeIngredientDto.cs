namespace Calories.API.Models.Dto
{
    public class RecipeIngredientDto
    {
        public int RecipeIngredientsId { get; set; }//Here
        public int RecipeId { get; set; }
        public string IngredientName { get; set; } = string.Empty;
        public double Quantity { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
