using Microsoft.AspNetCore.Identity;

namespace AuthApi.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string IdentityResetToken { get; set; } = "0";
        public string PasswordResetCode { get; set; } = "0";
        public DateTime? ResetTokenExpires { get; set; } = default!;
    }
}
