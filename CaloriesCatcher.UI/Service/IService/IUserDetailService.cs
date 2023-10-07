using KitchenComfort.Web.Models;

namespace CaloriesCatcher.UI.Service.IService
{
    public interface IUserDetailService
    {
        public Task<ResponseDto> GetByUser(string userId);
    }
}
