namespace CaloriesCatcher.UI.Model
{
    public class CaloriesDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public double Calorie { get; set; }
        public string CalorieName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
