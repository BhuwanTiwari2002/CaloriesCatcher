namespace AuthApi.API.Models;

public class EmailAuthOptions
{
    public string SmtpHost { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}