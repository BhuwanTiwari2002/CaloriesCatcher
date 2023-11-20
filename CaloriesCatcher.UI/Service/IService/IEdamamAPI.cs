using CaloriesCatcher.UI.Model;
using CaloriesCatcher.UI.Model.Edamam;

namespace CaloriesCatcher.UI.Service.IService
{
    interface IEdamamAPI
    {
        Task<Nutrition> GetNutrition(EdamamRequestDto edamamRequestDto, bool isNutritionDetails = false);
        Task<RecipeModelEdamam> GetRecipe(RecipeEdamaRequestDto recipeEdamaRequestDto);
    }
}
