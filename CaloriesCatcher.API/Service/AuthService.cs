using AuthApi.API.Data;
using AuthApi.API.Models;
using AuthApi.API.Service.IService;
using CaloriesCatcher.API.Models.Dto;
using KitchenComfort.Services.AuthAPI.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.Net;

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
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);
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
                user.PasswordResetCode = resetCode;
                user.IdentityResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                user.IdentityResetToken = ToBase64UrlString(user.IdentityResetToken);
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
        public async Task SendForgotPasswordEmail(ApplicationUser applicationUser, string resetCode)
        {
            var resetLink = $"https://localhost:7024/reset-password?token={applicationUser.IdentityResetToken}";
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
                            <p>Click the link below to reset your password:</p>
                            <a href='{resetLink}'>Reset Password</a>
                            <p>Once on the page, enter the following code:</p>
                            <h2>{resetCode}</h2>
                        </div>
                    </div>
                </body>
                </html>";
            await _emailSender.SendEmailAsync(applicationUser.Email, "Reset Password for Calories Tracker", message);
        }
        public async Task<string> ResetPassword(PasswordResetRequest request)
        {
            var user = _db.Users.FirstOrDefault(u =>
                u.Email == request.Email && u.PasswordResetCode == request.ResetCode && u.IdentityResetToken == request.Token);

            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return "Invalid or expired code/token";
            }

            var decodedToken = FromBase64UrlString(user.IdentityResetToken);
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, request.NewPassword);

            if (result.Succeeded)
            {
                user.IdentityResetToken = string.Empty;
                user.PasswordResetCode = string.Empty;
                user.ResetTokenExpires = null;
                await _db.SaveChangesAsync();
                return "Password reset successfully";
            }

            return "Failed to reset password";
        }
        private string GenerateUserCode()
        {
            var rng = new Random();
            return rng.Next(100000, 999999).ToString(); // Generates a 6-digit code.
        }
        public static string ToBase64UrlString(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            var base64Encoded = Convert.ToBase64String(inputBytes);
            return base64Encoded.TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }
        public static string FromBase64UrlString(string base64Url)
        {
            base64Url = base64Url.Replace('-', '+').Replace('_', '/');

            switch (base64Url.Length % 4)
            {
                case 2: base64Url += "=="; break;
                case 3: base64Url += "="; break;
            }

            var base64Bytes = Convert.FromBase64String(base64Url);
            return System.Text.Encoding.UTF8.GetString(base64Bytes);
        }
        public  List<UserDto> getAllUsers()
        {
            return _db.ApplicationUsers.ToList().Select(user => new UserDto
            {
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Id = user.Id
            }).ToList();
        }
        public async Task<string> DeleteUser(string id)
        {
            try
            {
                var user = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return "User Not Found";
                }
                var result = _userManager.DeleteAsync(user);
                return result.Result.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}