using AuthApi.API.Models;
using AuthApi.API.Service.IService;
using CaloriesCatcher.API.Models.Dto;
using KitchenComfort.Services.AuthAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : Controller
    {
        private readonly IAuthService _authService;
        protected ResponseDto _responseDto;
        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _responseDto = new();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDto registerationRequestDto)
        {
            // Rrgister
            var errorMessage = await _authService.Register(registerationRequestDto);
            var assignRoleSuccessful = await _authService.AssignRole(registerationRequestDto.Email, registerationRequestDto.RoleName.ToUpper());
            if (!string.IsNullOrEmpty(errorMessage) || assignRoleSuccessful == false)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = errorMessage;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var loginResponse = await _authService.Login(loginRequestDto);
            if (loginResponse.User == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Username or password is incorrect";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = loginResponse;
            return Ok(_responseDto);
        }
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegisterationRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.RoleName.ToUpper());
            if (!assignRoleSuccessful)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error encountered";
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var response = await _authService.ForgotPassword(dto.Email);
            if (response == "Success")
            {
                return Ok(new { Message = "An email with a reset code has been sent. Please check your email." });
            }
            else
            {
                return BadRequest(new { Message = response });
            }
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetRequest request)
        {
            var response = await _authService.ResetPassword(request);
            if (response == "Password reset successfully")
            {
                return Ok(new { Message = response });
            }
            else
            {
                return BadRequest(new { Message = response });
            }
        }
    }
    public class ForgotPasswordDto
    {
        public string Email { get; set; }
    }
    
}
