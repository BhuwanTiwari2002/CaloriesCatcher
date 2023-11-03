using CaloriesCatcher.UI.Model;
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

        public async Task<ResponseDto?> AssignRoleAsync(RegisterationRequestDto registerationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.POST,
                Data = registerationRequestDto,
                Url = StaticType.AuthAPIBase + "api/auth/AssignRole"
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.POST,
                Data = loginRequestDto,
                Url = StaticType.AuthAPIBase + "api/auth/login"
            });
        }
        public void StartGoogleLogin()
        {
            // Redirect to the API's Google Login endpoint to start the Google authentication process
            var googleLoginUrl = StaticType.AuthAPIBase + "api/signin-google/StartGoogleLogin";
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
                Url = StaticType.AuthAPIBase + "api/auth/register"
            });
        }
        public async Task<ResponseDto?> ForgotPasswordAsync(string email)
        {
            var requestDto = new RequestDto()
            {
                ApiType = StaticType.ApiType.POST,
                Data = new { Email = email },  // An anonymous type for the request body
                Url =  StaticType.AuthAPIBase + "api/auth/forgot-password"
            };

            return await _baseService.SendAsync(requestDto);
        }
        public async Task<ResponseDto?> ResetPasswordAsync(PasswordResetRequest request)
        {
            var requestDto = new RequestDto()
            {
                ApiType = StaticType.ApiType.POST,
                Data = request,
                Url = StaticType.AuthAPIBase + "api/auth/reset-password"
            };

            return await _baseService.SendAsync(requestDto);
        }
        public async Task<ResponseDto?> GetAllUsers()
        {
            var requestDto = new RequestDto()
            {
                ApiType = StaticType.ApiType.GET,
                Url = StaticType.AuthAPIBase + "api/auth/GetAll"
            };

            return await _baseService.SendAsync(requestDto);
        }

        public async Task<ResponseDto?> DeleteUser(string Id)
        {
            var requestDto = new RequestDto()
            {
                ApiType = StaticType.ApiType.DELETE,
                Url = StaticType.AuthAPIBase + $"auth/Delete/{Id}"
            };
            return await _baseService.SendAsync(requestDto);
        }
    }
}
