using CaloriesCatcher.UI.Model;
using CaloriesCatcher.UI.Model.Edamam;
using CaloriesCatcher.UI.Service.IService;
using KitchenComfort.Web.Models;
using static KitchenComfort.Web.Models.Utility.StaticType;

namespace CaloriesCatcher.UI.Service
{
    public class EdamamAPI : IEdamamAPI
    {
        private readonly IBaseService _baseService;
        public EdamamAPI(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<Nutrition> GetNutrition(EdamamRequestDto edamamRequestDto)
        {
            return await _baseService.SendAsyncEdamam(new RequestDto()
            {
                ApiType = ApiType.GET,
                Data = edamamRequestDto,
                Url = $"{edamamRequestDto.Url}?app_id={edamamRequestDto.app_id}&app_key={edamamRequestDto.app_key}&nutrition-type=cooking&ingr={edamamRequestDto.ingredient}"
            });
        }

        public async Task<List<RecipeModelEdamam>> GetRecipe(RecipeEdamaRequestDto recipeEdamaRequestDto)
        {
            return await _baseService.SendAsyncRecipeEdamam(new RequestDto()
            {
                ApiType = ApiType.GET,
                Data = recipeEdamaRequestDto,
                Url = $"{recipeEdamaRequestDto.Url}&q={recipeEdamaRequestDto.ReceipeName}&app_id={recipeEdamaRequestDto.ApplicationId}&app_key={recipeEdamaRequestDto.ApplicationKey}&field=image&field=calories"
                
            });        
        }
    }
}
