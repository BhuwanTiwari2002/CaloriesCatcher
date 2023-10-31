using CaloriesCatcher.UI.Model;
using CaloriesCatcher.UI.Service.IService;
using KitchenComfort.Web.Models;
using KitchenComfort.Web.Models.Utility;
using static System.Net.WebRequestMethods;

namespace CaloriesCatcher.UI.Service
{
    public class CaloriesService : ICalories
    {
        private readonly IBaseService _baseService;
        public CaloriesService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> CreateCaloriesAsync(CaloriesDto caloriesDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.POST,
                Data = caloriesDto,
                Url = "https://localhost:7005/api/calories"
            });
        }

        public async Task<ResponseDto> GetCaloriesByUserAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.GET,
                Url = $"https://localhost:7005/api/calories/{userId}"
            });
        }

        public async Task<ResponseDto> DeleteCaloriesAsync(int userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.DELETE,
                Url = $"https://localhost:7005/api/calories/{userId}"
            });
        }

        public async Task<ResponseDto> UpdateCaloriesAsync(CaloriesDto caloriesDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.PUT,
                Data = caloriesDto,
                Url = "https://localhost:7005/api/calories"
            });
        }

        public async Task<ResponseDto> GetAllCaloriesAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.GET,
                Url = "https://localhost:7005/api/calories"
            });
        }
    }
}
