namespace CaloriesCatcher.UI.Model;

public class Subscription
{
    public string Name { get; set; }
    public decimal MonthlyPrice { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
