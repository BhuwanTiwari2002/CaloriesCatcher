using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CaloriesCatcher.UI.Service.IService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using KitchenComfort.Web.Models;
using KitchenComfort.Web.Models.Utility;

namespace CaloriesCatcher.UI.Service
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        private readonly IJsInteropService _jsInteropService;

        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage, IJsInteropService jsInteropService)
        {
            _sessionStorage = sessionStorage;
            _jsInteropService = jsInteropService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                // Read token from the cookie
                var token = await _jsInteropService.GetCookieAsync(StaticType.TokenCookie);
                if (string.IsNullOrEmpty(token))
                {
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }

                // Decode the token and build the ClaimsPrincipal
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                // Add claims from JWT
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                    jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)?.Value));
                identity.AddClaim(new Claim(ClaimTypes.Email,
                    jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)?.Value));
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                    jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value));
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                    jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name)?.Value));
                identity.AddClaim(new Claim(ClaimTypes.Name,
                    jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name)?.Value));
                identity.AddClaim(new Claim(ClaimTypes.Role,
                    jwt.Claims.FirstOrDefault(u => u.Type == "role")?.Value));
                var claimsPrincipal = new ClaimsPrincipal(identity);
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }
        public async Task UpdateAuthenticationState(LoginResponseDto loginResponse)
        {
            if (loginResponse != null)
            {
                // Check if User object or its properties are null
                if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.User.Name) || string.IsNullOrEmpty(loginResponse.User.Email))
                {
                    // Log the error or throw an exception
                    throw new ArgumentNullException("User object or its properties are null");
                }

                // Store JWT token and user details in session storage
                await _sessionStorage.SetAsync(StaticType.LoginSession, loginResponse);
                await _sessionStorage.SetAsync(StaticType.TokenCookie, loginResponse.Token);

                // Decode JWT and create ClaimsIdentity
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(loginResponse.Token);
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                // Add claims from JWT
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                    jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)?.Value));
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                    jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value));
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                    jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name)?.Value));
                identity.AddClaim(new Claim(ClaimTypes.Name,
                    jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)?.Value));
                identity.AddClaim(new Claim(ClaimTypes.Role,
                    jwt.Claims.FirstOrDefault(u => u.Type == "role")?.Value));
  
                var claimsPrincipal = new ClaimsPrincipal(identity);

                // Set the JWT token as a persistent cookie that lasts for 7 days
                await _jsInteropService.SetCookieAsync(StaticType.TokenCookie, loginResponse.Token, 7);

                // Notify the authentication state has changed
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
            }
            else
            {
                // Delete session storage and session cookie
                await _sessionStorage.DeleteAsync(StaticType.LoginSession);
                await _jsInteropService.DeleteCookieAsync(StaticType.TokenCookie);

                // Notify the authentication state has changed
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
            }
        }
        public async Task HandleGoogleLogin(string googleJwtToken)
        {
            if (string.IsNullOrEmpty(googleJwtToken))
            {
                // Log the error or throw an exception
                throw new ArgumentNullException("Token is null or empty");
            }

            // Decode JWT and create ClaimsIdentity
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(googleJwtToken);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            // Add claims from JWT
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)?.Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name)?.Value));
            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)?.Value));

            var claimsPrincipal = new ClaimsPrincipal(identity);

            // Set the JWT token as a persistent cookie that lasts for 7 days
            await _jsInteropService.SetCookieAsync(StaticType.TokenCookie, googleJwtToken, 7);

            // Notify the authentication state has changed
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }


    }
}
