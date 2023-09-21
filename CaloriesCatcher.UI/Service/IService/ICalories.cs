using KitchenComfort.Web.Models;

namespace CaloriesCatcher.UI.Service.IService
{
    public interface ICalories
    {
        Task<ResponseDto> GetAllCaloriesAsync();

    }
}
