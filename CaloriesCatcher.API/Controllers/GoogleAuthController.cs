using AuthApi.API.Models;
using AuthApi.API.Service.IService;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/signin-google")]
[ApiController]
public class GoogleAuthController : ControllerBase
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    public GoogleAuthController(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    [HttpGet("StartGoogleLogin")]
    public IActionResult StartGoogleLogin()
    {
        // Starts the Google authentication process
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
            var jwtToken = _jwtTokenGenerator.GenerateToken(applicationUser);
            return Redirect($"https://localhost:7024/?token={jwtToken}");
        }

        return BadRequest("Google authentication failed.");
    }
}
