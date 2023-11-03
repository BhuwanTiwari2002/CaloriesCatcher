using CaloriesCatcher.UI.Service.IService;
using KitchenComfort.Web.Models.Utility;
using KitchenComfort.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaloriesCatcher.UI.Service
{
    public class RecipeIngredientsService : Controller
    {
        private readonly IBaseService _baseService;
        public RecipeIngredientsService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> CreateRecipeIngredientAsync(CaloriesCatcher.UI.Model.RecipeIngredientDto recipeIngredientDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.POST,
                Data = recipeIngredientDto,
                Url = "https://localhost:7005/api/recipeingredients"
            });
        }

        public async Task<ResponseDto> GetAllRecipeIngredientsAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.GET,
                Url = $"https://localhost:7005/api/recipeingredients/{userId}"
            });
        }

        public async Task<ResponseDto> DeleteRecipeIngredientAsync(int userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.DELETE,
                Url = $"https://localhost:7005/api/recipeingredients/{userId}"
            });
        }

        public async Task<ResponseDto> UpdateRecipeIngredientAsync(CaloriesCatcher.UI.Model.RecipeIngredientDto recipeIngredientDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.PUT,
                Data = recipeIngredientDto,
                Url = "https://localhost:7005/api/recipeingredients"
            });
        }

        public async Task<ResponseDto> GetAllRecipesAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.GET,
                Url = "https://localhost:7005/api/recipeingredients"
            });
        }
    }
}
