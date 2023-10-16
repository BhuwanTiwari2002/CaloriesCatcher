namespace CaloriesCatcher.UI.Model;

public class UserDetail
{
    public int Id { get; set; }// = 0;
    public string UserId { get; set; } = string.Empty;
    public int Height { get; set; } //= 0;
    public int Weight { get; set; } //= 0;
    public Decimal DailyCalories { get; set; } //= 0.00m;
    public DateTime BirthDate { get; set; }// = new DateTime();
    public string Gender { get; set; } = string.Empty;
    public int? CalorieId { get; set; }// = 0;
    public int? RecipeFoodId { get; set; }// = 0;
}