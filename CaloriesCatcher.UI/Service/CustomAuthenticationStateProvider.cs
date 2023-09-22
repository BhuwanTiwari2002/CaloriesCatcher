using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using KitchenComfort.Web.Models;
using KitchenComfort.Web.Models.Utility;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CaloriesCatcher.UI.Service
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSessionStorageResult = await _sessionStorage.GetAsync<LoginResponseDto>(StaticType.LoginSession);
                var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
                if (userSession == null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.User.Id),
                    new Claim(ClaimTypes.Email, userSession.User.Email)
                }, "CalorieCatcherAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public async Task UpdateAuthenticationState(LoginResponseDto loginResponse)
        {
            ClaimsPrincipal claimsPrincipal;
            if (loginResponse != null)
            {
                // Check if User object or its properties are null
                if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.User.Name) || string.IsNullOrEmpty(loginResponse.User.Email))
                {
                    // Log the error or throw an exception
                    throw new ArgumentNullException("User object or its properties are null");
                }

                await _sessionStorage.SetAsync(StaticType.LoginSession, loginResponse);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginResponse.User.Id),
                    new Claim(ClaimTypes.Email, loginResponse.User.Email)
                }));
            }
            else
            {
                await _sessionStorage.DeleteAsync(StaticType.LoginSession);
            }
        }

    }
}
