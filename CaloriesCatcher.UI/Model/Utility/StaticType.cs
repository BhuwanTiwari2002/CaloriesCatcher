namespace KitchenComfort.Web.Models.Utility
{
    public class StaticType
    {
        public static string AuthAPIBase { get; set; } = "https://bhupintiwari-001-site1.gtempurl.com/";
        public static string CaloriesAPIBase { get; set; } = "https://localhost:7005/";
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
