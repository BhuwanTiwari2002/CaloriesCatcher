namespace CaloriesCatcher.API.Models.Dto
{
    public class RegisterationRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
