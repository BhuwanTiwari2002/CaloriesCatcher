using CaloriesCatcher.UI.Service.IService;
using KitchenComfort.Web.Models;
using KitchenComfort.Web.Models.Utility;

namespace CaloriesCatcher.UI.Service
{
    public class CaloriesService : ICalories
    {
        private readonly IBaseService _baseService;
        public CaloriesService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto> GetAllCouponAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.GET,
                Url = "https://localhost:7005/api/calories"
            });
        }
    }
}
