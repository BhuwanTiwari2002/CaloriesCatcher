

namespace Calories.API.Models.Dto
{
    public class AspNetUsersDetailDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int DailyCalories { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        public int CalorieId { get; set; }
        public int RecipeFoodId { get; set; }
    }
}
