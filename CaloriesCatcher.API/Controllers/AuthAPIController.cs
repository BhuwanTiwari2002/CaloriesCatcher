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
        private readonly ILogger<AuthAPIController> _logger;

        public AuthAPIController(IAuthService authService, ILogger<AuthAPIController> logger)
        {
            _authService = authService;
            _responseDto = new();
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDto registerationRequestDto)
        {
            var errorMessage = await _authService.Register(registerationRequestDto);
            var assignRoleSuccessful = await _authService.AssignRole(registerationRequestDto.Email,
                registerationRequestDto.RoleName.ToUpper());
            if (!string.IsNullOrEmpty(errorMessage) || assignRoleSuccessful == false)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = errorMessage;
                _logger.LogInformation(message: $"Error occurred when registering. Error Message: {errorMessage}");
                return BadRequest(_responseDto);
            }

            _logger.LogInformation(message: $"Successfully Registered.");
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
                _logger.LogInformation(message: $"Error occurred when attempting to login.");
                return BadRequest(_responseDto);
            }

            _responseDto.Result = loginResponse;
            _logger.LogInformation(message: $"Successful Login");
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
                _logger.LogInformation(message: $"Error occurred while attempting to assign role.");
                return BadRequest(_responseDto);
            }

            _logger.LogInformation(message: $"Successfully assigned role.");
            return Ok(_responseDto);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var response = await _authService.ForgotPassword(dto.Email);
            if (response == "Success")
            {
                _logger.LogInformation(message: $"Successful sent password reset link.");
                return Ok(new { Message = "An email with a reset code has been sent. Please check your email." });
            }
            else
            {
                _logger.LogInformation(message: $"Error occurred when attempting to send reset password link.");
                return BadRequest(new { Message = response });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetRequest request)
        {
            var response = await _authService.ResetPassword(request);
            if (response == "Password reset successfully")
            {
                _logger.LogInformation(message: $"Seccessful password reset.");
                return Ok(new { Message = response });
            }
            else
            {
                _logger.LogInformation(message: $"Error occurred when attempting to reset password.");
                return BadRequest(new { Message = response });
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllUsers()
        {
            var response =  _authService.getAllUsers();
            if (response == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Cannot Find users";
                _logger.LogInformation(message: $"Error occurred when fetching all users.");
                return BadRequest(_responseDto);
            }
            _responseDto.Result = response;
            _logger.LogInformation(message: $"Successfully fetched all users.");
            return Ok(_responseDto);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var response = await _authService.DeleteUser(id);
            if (response == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Something went wrong";
                _logger.LogInformation(message: $"Error occurred when attempting to delete UserId: {id}.");
                return BadRequest(_responseDto);
            }
            _responseDto.Result = response;
            _logger.LogInformation(message: $"Successfully deleted UserId: {id}");
            return Ok(_responseDto);
        }

        [HttpGet("GetUserById/{id}")]
        public IActionResult GetUserById(string id)
        {
            var response = _authService.getUserById(id);
            if (response == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Something went wrong";
                _logger.LogInformation(message: $"Error occurred when attempting to fetch UserId: {id}.");
                return BadRequest(_responseDto); 
            }
            _responseDto.Result = response;
            _logger.LogInformation(message: $"Successfully fetched UserId: {id}");
            return Ok(_responseDto); 
        }      
    }
    
    public class ForgotPasswordDto
    {
        public string Email { get; set; }
    }    
}
