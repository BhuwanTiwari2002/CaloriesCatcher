using CaloriesCatcher.UI.Model;
using KitchenComfort.Web.Models;

namespace CaloriesCatcher.UI.Service.IService
{
    public interface IUserDetailService
    {
        Task<ResponseDto> GetByUser(string userId);
        Task<ResponseDto> CreateUserDetailsAsync(UserDetail userDetail);
        Task<ResponseDto> UpdateUserDetailsAsync(UserDetail userDetail);
    }
}
