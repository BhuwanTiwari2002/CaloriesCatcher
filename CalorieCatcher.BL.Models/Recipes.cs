namespace CalorieCatcher.BL.Models
{
    public class Recipes
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }

        //These are commented because im not sure they need to be here
        //pretty sure we will be able to just calculate this on the page itself
        //by just adding all the RecipeFood items together
        //call all of the RecipeFood items with this recipe Id and add all of the calorie fields together

        //public int Calories { get; set; }
        //public int RecipeNutrition { get; set; }
    }
}