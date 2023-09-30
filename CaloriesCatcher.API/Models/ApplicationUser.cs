using Microsoft.AspNetCore.Identity;

namespace AuthApi.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string PasswordResetToken { get; set; } = String.Empty;
        public DateTime? ResetTokenExpires { get; set; } = default!;
    }
}
