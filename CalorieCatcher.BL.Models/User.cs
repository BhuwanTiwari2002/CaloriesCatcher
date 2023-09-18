namespace CalorieCatcher.BL.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public string NormUsername { get; set; }
        public string NormEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public int LogoutEnd { get; set; }
        public bool LogoutEnabled { get; set; }
        public int AccessFailCount { get; set; }
    }
}