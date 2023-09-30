namespace CaloriesCatcher.UI.Model
{
    public class PasswordResetRequest
    {
        public string Email { get; set; }
        public string ResetCode { get; set; }
        public string NewPassword { get; set; }
        public string Token { get; set; }
    }

}
