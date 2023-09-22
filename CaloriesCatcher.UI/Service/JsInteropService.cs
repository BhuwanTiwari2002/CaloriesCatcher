using Microsoft.JSInterop;
using CaloriesCatcher.UI.Service.IService;

namespace CaloriesCatcher.UI.Service
{
    public class JsInteropService : IJsInteropService
    {
        private readonly IJSRuntime _jsRuntime;

        public JsInteropService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SetCookieAsync(string name, string value, int days)
        {
            await _jsRuntime.InvokeVoidAsync("setCookie", name, value, days);
        }

        public async Task DeleteCookieAsync(string name)
        {
            await _jsRuntime.InvokeVoidAsync("deleteCookie", name);
        }

        public async Task SetSessionCookieAsync(string name, string value)
        {
            await _jsRuntime.InvokeVoidAsync("setSessionCookie", name, value);
        }
        
        public async Task<string> GetCookieAsync(string name)
        {
            return await _jsRuntime.InvokeAsync<string>("getCookie", name);
        }

    }
}