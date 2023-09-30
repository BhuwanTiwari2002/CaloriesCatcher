using AuthApi.API.Data;
using AuthApi.API.Models;
using AuthApi.API.Service.IService;
using CaloriesCatcher.API.Models.Dto;
using KitchenComfort.Services.AuthAPI.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace AuthApi.API.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IEmailSender _emailSender;

        public AuthService(AppDbContext db, IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IEmailSender emailSender)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _emailSender = emailSender;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                /* With the Awaiter, you don't need to make the method an async */
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    // Create role if it does not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }

                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }

            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u =>
                u.Email.ToLower() == loginRequestDto.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (isValid == false || user == null)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            // Generate JWT Token
            var token = _jwtTokenGenerator.GenerateToken(user);
            UserDto userDto = new()
            {
                Email = user.Email,
                Id = user.Id,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber
            };
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };
            return loginResponseDto;
        }

        public async Task<string> Register(RegisterationRequestDto registerationRequestDto)
        {
            ApplicationUser applicationUser = new()
            {
                UserName = registerationRequestDto.UserName,
                Email = registerationRequestDto.Email,
                NormalizedEmail = registerationRequestDto.Email.ToUpper(),
                PhoneNumber = registerationRequestDto.PhoneNumber,
                NormalizedUserName = registerationRequestDto.UserName
            };
            try
            {
                var result = await _userManager.CreateAsync(applicationUser, registerationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn =
                        _db.ApplicationUsers.FirstOrDefault(u => u.Email == registerationRequestDto.Email);
                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        Id = userToReturn.Id,
                        UserName = userToReturn.UserName,
                        PhoneNumber = userToReturn.PhoneNumber
                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
            }

            return "Error Encountered";
        }

        public async Task<string> ForgotPassword(string email)
        {
            try
            {
                var user = _db.Users.FirstOrDefault(u => u.Email == email);
                if (user == null)
                {
                    return "User not found";
                }

                var resetCode = GenerateUserCode();
                user.PasswordResetToken = resetCode;
                user.ResetTokenExpires = DateTime.Now.AddHours(1);

                await _db.SaveChangesAsync();
                await SendForgotPasswordEmail(user, resetCode);

                return "Success";
            }
            catch (Exception ex)
            {
                return $"Failed {ex}";
            }
        }

        public async Task<string> ResetPassword(PasswordResetRequest request)
        {
            var user = _db.Users.FirstOrDefault(u =>
                u.Email == request.UserId && u.PasswordResetToken == request.ResetCode);
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return "Invalid or expired code";
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result =
                await _userManager.ResetPasswordAsync(user, token, request.Token); // Using Token as the new password

            if (result.Succeeded)
            {
                user.PasswordResetToken = null; // Clear the reset code
                user.ResetTokenExpires = null; // Clear the expiration
                await _db.SaveChangesAsync();

                return "Password reset successfully";
            }

            return "Failed to reset password";
        }

        public async Task SendForgotPasswordEmail(ApplicationUser applicationUser, string resetCode)
        {
            var message = $@"
                <html>
                <head>
                    <style>
                        .container {{ font-family: Arial, sans-serif; padding: 20px; border: 1px solid #e0e0e0; }}
                        .header {{ background-color: #4CAF50; color: white; padding: 10px 0; text-align: center; }}
                        .content {{ padding: 20px; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>Calories Tracker</div>
                        <div class='content'>
                            <p>Your password reset code is:</p>
                            <h2>{resetCode}</h2>
                        </div>
                    </div>
                </body>
                </html>";
            await _emailSender.SendEmailAsync(applicationUser.Email, "Reset Password for Calories Tracker", message);
        }

        private string GenerateUserCode()
        {
            var rng = new Random();
            return rng.Next(100000, 999999).ToString(); // Generates a 6-digit code.
        }
    }
}