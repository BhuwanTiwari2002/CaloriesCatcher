namespace KitchenComfort.Web.Models.Utility
{
    public class StaticType
    {
        public static string CouponAPIBase { get; set; }
        public static string AuthAPIBase { get; set; }
        public static string TokenCookie { get; set; } = "JWTToken";
        public enum ApiType
        {
            GET,
            POST, 
            PUT, 
            DELETE
        }
    }
}
