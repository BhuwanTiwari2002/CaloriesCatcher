namespace KitchenComfort.Web.Models
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; } = String.Empty;

        public string PhoneNumber { get; set; }
    }
}
