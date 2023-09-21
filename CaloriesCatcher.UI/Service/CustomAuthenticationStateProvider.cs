using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace CaloriesCatcher.UI.Service
{
    public class CustomAuthenticationStateProvider
    {
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        //public override Task<AuthenticationState> GetAuthenticationStateAsync()
        //{

        //}
    }
}
