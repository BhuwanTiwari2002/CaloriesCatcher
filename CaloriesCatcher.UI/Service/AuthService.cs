using CaloriesCatcher.UI.Service.IService;
using KitchenComfort.Web.Models;

namespace CaloriesCatcher.UI.Service
{
    public class AuthService : IAuthService
    {
        //private readonly IBaseService _baseService;
        public Task<ResponseDto?> AssignRoleAsync(RegisterationRequestDto registerationRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> RegisterAsync(RegisterationRequestDto registerationRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
