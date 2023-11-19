using AuthApi.API.Models;
using AuthApi.API.Service.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

[Route("api/signin-google")]
[ApiController]
public class GoogleAuthController : ControllerBase
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<GoogleAuthController> _logger;
 
    public GoogleAuthController(IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, ILogger<GoogleAuthController> logger)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userManager = userManager;
        _logger = logger;
    }

    [HttpGet("StartGoogleLogin")]
    public IActionResult StartGoogleLogin()
    {
        // Starts the Google authentication process
        _logger.LogInformation(message: $"Attempting Google authentication process");
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = "/api/signin-google/GoogleLogin"
        }, "Google");

    }

    [HttpGet("GoogleLogin")]
    public async Task<IActionResult> GoogleLogin()
    {
        var authResult = await HttpContext.AuthenticateAsync("Google");
        if (!authResult.Succeeded)
        {
            var error = authResult.Failure?.Message; 
            _logger.LogInformation(message: $"Error occurred when attempting to authenticate with Google. Error Message: {error}");
            return BadRequest($"Google authentication failed. Reason: {error}");
        }

        var claimsPrincipal = authResult.Principal;

        if (claimsPrincipal?.Identity?.IsAuthenticated == true)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                Email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value,
                UserName = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value,
                Id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value
            };

            _logger.LogInformation(message: $"Successfully authenticated with Google.");
            // Add the "Basic" role to the user.
            List<string> roles = new List<string> { "TURTLE" };
            var jwtToken = _jwtTokenGenerator.GenerateToken(applicationUser, roles);
            _logger.LogInformation(message: $"Successfully assigned new role to user. Role Assigned: {roles}");
            return Redirect($"https://localhost:7024/login?token={jwtToken}");
        }

        _logger.LogInformation(message: $"Error occurred when attempting to authenticate with Google.");
        return BadRequest("Google authentication failed.");
    }
}
