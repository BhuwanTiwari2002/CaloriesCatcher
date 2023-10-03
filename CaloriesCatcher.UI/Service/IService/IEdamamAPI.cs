using CaloriesCatcher.UI.Model;

namespace CaloriesCatcher.UI.Service.IService
{
    interface IEdamamAPI
    {
        Task<Nutrition> GetNutrition(EdamamRequestDto edamamRequestDto);
    }
}
