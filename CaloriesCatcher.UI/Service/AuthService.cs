using CaloriesCatcher.UI.Service.IService;
using KitchenComfort.Web.Models;
using KitchenComfort.Web.Models.Utility;

namespace CaloriesCatcher.UI.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public Task<ResponseDto?> AssignRoleAsync(RegisterationRequestDto registerationRequestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.POST,
                Data = loginRequestDto,
                Url = "https://localhost:7002/api/auth/login"
            });
        }
        public void StartGoogleLogin()
        {
            // Redirect to the API's Google Login endpoint to start the Google authentication process
            var googleLoginUrl = "https://localhost:7002/api/signin-google/StartGoogleLogin";
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = googleLoginUrl,
                UseShellExecute = true
            });
        }



        public async Task<ResponseDto?> RegisterAsync(RegisterationRequestDto registerationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.POST,
                Data = registerationRequestDto,
                Url = "https://localhost:7002/api/auth/register"
            });
        }
    }
}
