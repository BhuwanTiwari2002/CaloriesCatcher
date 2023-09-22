namespace CaloriesCatcher.UI.Service.IService
{
    public interface IJsInteropService
    {
        Task SetCookieAsync(string name, string value, int days);
        Task DeleteCookieAsync(string name);
        Task SetSessionCookieAsync(string name, string value);
        Task<string> GetCookieAsync(string name);

    }
}
