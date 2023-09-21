using CaloriesCatcher.UI.Service.IService;
using KitchenComfort.Web.Models.Utility;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
namespace CaloriesCatcher.UI.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IJsInteropService _jsInteropService;
        public TokenProvider(IHttpContextAccessor contextAccessor, IJsInteropService jsInteropService)
        {
            _contextAccessor = contextAccessor;
            _jsInteropService = jsInteropService;
        }

        public async void ClearToken()
        {
            await _jsInteropService.DeleteCookieAsync(StaticType.TokenCookie);
        }

        public string? GetToken()
        {
           string? token = null;
           bool? hasToken = (_contextAccessor.HttpContext?.Request.Cookies.TryGetValue(StaticType.TokenCookie, out token));
           return hasToken is true ? token : null;
        }

        public async void SetToken(string token)
        {
            await _jsInteropService.SetCookieAsync(StaticType.TokenCookie, token, 7);
        }
    }
}
