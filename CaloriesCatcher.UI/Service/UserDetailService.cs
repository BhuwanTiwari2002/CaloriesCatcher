using CaloriesCatcher.UI.Model;
using CaloriesCatcher.UI.Service.IService;
using KitchenComfort.Web.Models;
using KitchenComfort.Web.Models.Utility;
using static System.Net.WebRequestMethods;

namespace CaloriesCatcher.UI.Service
{
    public class UserDetailService : IUserDetailService
    {
        private readonly IBaseService _baseService;
        public UserDetailService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public Task<ResponseDto> CreateUserDetailsAsync(UserDetail userDetail)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto> GetByUser(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.GET,
                Url = $"https://localhost:7005/api/profile/{userId}"
            });

        }
    }
}
