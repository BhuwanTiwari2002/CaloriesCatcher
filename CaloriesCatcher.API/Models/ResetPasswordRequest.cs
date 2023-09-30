namespace AuthApi.API.Models;

public class PasswordResetRequest
{
    public int Id { get; set; }
    public string UserId { get; set; } // Reference to your user entity.
    public string ResetCode { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiryTime { get; set; }
}
