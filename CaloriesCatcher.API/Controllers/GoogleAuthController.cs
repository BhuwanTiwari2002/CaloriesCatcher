﻿using AuthApi.API.Models;
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
 
    public GoogleAuthController(IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userManager = userManager;
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
            var roles = await _userManager.GetRolesAsync(applicationUser);
            var jwtToken = _jwtTokenGenerator.GenerateToken(applicationUser, roles);
            return Redirect($"https://localhost:7024/login?token={jwtToken}");
        }

        return BadRequest("Google authentication failed.");
    }
}
