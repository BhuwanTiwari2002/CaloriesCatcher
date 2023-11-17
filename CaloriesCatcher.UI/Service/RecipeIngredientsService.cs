using CaloriesCatcher.UI.Service.IService;
using KitchenComfort.Web.Models.Utility;
using KitchenComfort.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaloriesCatcher.UI.Service
{
    public class RecipeIngredientsService : IRecipeIngredients
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
                Url = StaticType.CaloriesAPIBase + "api/recipeingredients"
            }).ConfigureAwait(false);
        }

        public async Task<ResponseDto> DeleteRecipeIngredientAsync(int userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.DELETE,
                Url = StaticType.CaloriesAPIBase + $"api/recipeingredients/{userId}"
            }).ConfigureAwait(false);
        }

        public async Task<ResponseDto> UpdateRecipeIngredientAsync(CaloriesCatcher.UI.Model.RecipeIngredientDto recipeIngredientDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.PUT,
                Data = recipeIngredientDto,
                Url = StaticType.CaloriesAPIBase + "api/recipeingredients"
            }).ConfigureAwait(false);
        }

        public async Task<ResponseDto> GetAllRecipeIngredientsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.GET,
                Url = StaticType.CaloriesAPIBase + "api/recipeingredients"
            }).ConfigureAwait(false);
        }
    }
}
