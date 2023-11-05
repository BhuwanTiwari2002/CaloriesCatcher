using AuthApi.API.Models;
using CaloriesCatcher.API.Models.Dto;
using KitchenComfort.Services.AuthAPI.Models.Dto;

namespace AuthApi.API.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegisterationRequestDto registerationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
        Task<string> ForgotPassword(string email);
        Task<string> ResetPassword(PasswordResetRequest request);
        List<UserDto> getAllUsers();
        Task<string> DeleteUser(string id);
        UserDto getUserById(string id);
    }
}
