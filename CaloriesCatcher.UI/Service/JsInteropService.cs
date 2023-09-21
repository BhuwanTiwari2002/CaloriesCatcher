using CaloriesCatcher.UI.Service.IService;
using Microsoft.JSInterop;

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
    }
}
