using CaloriesCatcher.UI.Model;
using CaloriesCatcher.UI.Model.Edamam;
using KitchenComfort.Web.Models;

namespace CaloriesCatcher.UI.Service.IService
{
    interface IEdamamAPI
    {
        Task<Nutrition> GetNutrition(EdamamRequestDto edamamRequestDto);
        Task<RecipeModelEdamam> GetRecipe(RecipeEdamaRequestDto recipeEdamaRequestDto);
        Task<Nutrition> GetNutritionForRecipe(EdamamRequestDto edamamRequestDto);


    }
}
