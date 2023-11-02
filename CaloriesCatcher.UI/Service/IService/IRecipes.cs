using CaloriesCatcher.UI.Model;
using CaloriesCatcher.UI.Model;
using KitchenComfort.Web.Models;

namespace CaloriesCatcher.UI.Service.IService
{
    public interface IRecipe
    {
        Task<ResponseDto> GetAllRecipesAsync();
        Task<ResponseDto> CreateRecipeAsync(RecipeDto recipeDto);
        Task<ResponseDto> GetRecipesByUserAsync(string UserId);
        Task<ResponseDto> DeleteRecipeAsync(int id);
        Task<ResponseDto> UpdateRecipeAsync(RecipeDto calorieDto);
    }
}
