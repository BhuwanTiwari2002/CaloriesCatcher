using CaloriesCatcher.UI.Model;
using KitchenComfort.Web.Models;

namespace CaloriesCatcher.UI.Service.IService
{
    public interface IRecipeIngredients
    {
        Task<ResponseDto> GetAllRecipeIngredientsAsync();
        Task<ResponseDto> CreateRecipeIngredientAsync(RecipeIngredientDto recipeIngredientsDto);
        Task<ResponseDto> GetRecipeIngredientsByUserAsync(string UserId);
        Task<ResponseDto> DeleteRecipeIngredientAsync(int id);
        Task<ResponseDto> UpdateRecipeIngredientAsync(RecipeIngredientDto recipeIngredientsDto);
    }
}
