using CaloriesCatcher.UI.Model;
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
        public Task<Nutrition> GetNutrition(EdamamRequestDto edamamRequestDto)
        {
            return await _baseService.SendAsyncEdamam(new RequestDto()
            {
                ApiType = ApiType.GET,
                Data = edamamRequestDto,
                Url = $"{edamamRequestDto.Url}?app_id={edamamRequestDto.app_id}&app_key={edamamRequestDto.app_key}&nutrition-type=cooking&ingr={edamamRequestDto.ingredient}"
            });
        }
    }
}
