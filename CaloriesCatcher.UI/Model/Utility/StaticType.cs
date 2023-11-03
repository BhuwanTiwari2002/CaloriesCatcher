namespace KitchenComfort.Web.Models.Utility
{
    public class StaticType
    {
        public static string AuthAPIBase { get; set; } = "https://localhost:7002/";
        public static string TokenCookie { get; set; } = "JWTToken";
        public static string LoginSession { get; set; } = "UserSession";

        public enum ApiType
        {
            GET,
            POST, 
            PUT, 
            DELETE
        }
    }
}
