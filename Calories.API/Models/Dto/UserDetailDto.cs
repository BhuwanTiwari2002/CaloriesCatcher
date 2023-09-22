
namespace Calories.API.Models.Dto
{
    public class UserDetailDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public double Height { get; set; }
        public double Weight { get; set; }
        public int DailyCalories { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        public int CalorieId { get; set; }
        public int RecipeFoodId { get; set; }
    }
}
