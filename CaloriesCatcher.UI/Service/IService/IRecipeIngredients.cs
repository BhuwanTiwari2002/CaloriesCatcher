using Calories.API.Models.Dto;

namespace CaloriesCatcher.UI.Service.IService
{
    public interface IRecipeIngredients
    {
        Task<ResponseDto> GetAllRecipeIngredientsAsync();
        Task<ResponseDto> CreateRecipeIngredientAsync(IRecipeIngredients recipeIngredientsDto);
        Task<ResponseDto> GetRecipeIngredientsByUserAsync(string UserId);
        Task<ResponseDto> DeleteRecipeIngredientAsync(int id);
        Task<ResponseDto> UpdateRecipeIngredientAsync(IRecipeIngredients recipeIngredientsDto);
    }
}
