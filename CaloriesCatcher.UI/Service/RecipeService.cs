using KitchenComfort.Web.Models;
using KitchenComfort.Web.Models.Utility;
using CaloriesCatcher.UI.Service.IService;
using CaloriesCatcher.UI.Model;

namespace RecipeCatcher.UI.Service
{
    public class RecipeService : IRecipe
    {
        private readonly IBaseService _baseService;
        public RecipeService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> CreateRecipeAsync(CaloriesCatcher.UI.Model.RecipeDto recipeDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.POST,
                Data = recipeDto,
                Url = StaticType.CaloriesAPIBase + "api/recipes"
            }).ConfigureAwait(false);
        }

        public async Task<ResponseDto> GetRecipesByUserAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.GET,
                Url = StaticType.CaloriesAPIBase + $"api/recipes/{userId}"
            }).ConfigureAwait(false);
        }

        public async Task<ResponseDto> DeleteRecipeAsync(int userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.DELETE,
                Url = StaticType.CaloriesAPIBase + $"api/recipes/{userId}"
            }).ConfigureAwait(false);
        }

        public async Task<ResponseDto> UpdateRecipeAsync(CaloriesCatcher.UI.Model.RecipeDto recipeDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.PUT,
                Data = recipeDto,
                Url = StaticType.CaloriesAPIBase + "api/recipes"
            }).ConfigureAwait(false);
        }

        public async Task<ResponseDto> GetAllRecipesAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.GET,
                Url = StaticType.CaloriesAPIBase + "api/recipes"
            }).ConfigureAwait(false);
        }
    }
}
