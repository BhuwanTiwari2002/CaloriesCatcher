using KitchenComfort.Web.Models.Utility;
using static KitchenComfort.Web.Models.Utility.StaticType;

namespace CaloriesCatcher.UI.Model
{
    public class EdamamRecipeNutritionDto
    {
        public string app_id { get; set; }
        public string app_key { get; set; }
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string RecipeTitle { get; set; }
        public string IngredientQuantity { get; set; }
        public string Url { get; set; }
        public object Data { get; set; }
        public EdamamRecipeNutritionDto(string data)
        {
            app_id = "d40eca51";
            app_key = "192fe1bbaeed7914764ddd4a4348e10c";
            this.Data = data;
            Url = "https://api.edamam.com/api/nutrition-data";
        }
    }
}
