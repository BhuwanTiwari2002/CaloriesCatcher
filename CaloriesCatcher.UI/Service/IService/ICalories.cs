using CaloriesCatcher.UI.Model;
using KitchenComfort.Web.Models;

namespace CaloriesCatcher.UI.Service.IService
{
    public interface ICalories
    {
        Task<ResponseDto> GetAllCaloriesAsync();
        Task<ResponseDto> CreateCaloriesAsync(CaloriesDto caloriesDto);
        Task<ResponseDto> GetCaloriesByUserAsync(string UserId);
        Task<ResponseDto> GetCaloriesByUserAsync(string userId, int filterOption);
        Task<ResponseDto> DeleteCaloriesAsync(int id);
        Task<ResponseDto> UpdateCaloriesAsync(CaloriesDto caloriesDto);
    }
}
