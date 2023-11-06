using CaloriesCatcher.UI.Model;
using CaloriesCatcher.UI.Model.Edamam;
using KitchenComfort.Web.Models;

namespace CaloriesCatcher.UI.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
        Task<Nutrition?> SendAsyncEdamam(RequestDto requestDto);
        Task<List<RecipeModelEdamam>> SendAsyncRecipeEdamam(RequestDto requestDto);
    }
}
