namespace CaloriesCatcher.UI.Model
{
    public class Nutrition
    {
        public string Uri { get; set; }
        public int Calories { get; set; }
        public double TotalCO2Emissions { get; set; }
        public string Co2EmissionsClass { get; set; }
        public double TotalWeight { get; set; }
        public List<string> DietLabels { get; set; }
        public List<string> HealthLabels { get; set; }
        public List<string> Cautions { get; set; }
        public Dictionary<string, Nutrient> TotalNutrients { get; set; }
        public Dictionary<string, DailyValue> TotalDaily { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public Dictionary<string, NutrientKCal> TotalNutrientsKCal { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
    public class Nutrient
    {
        public string Label { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
    }
    public class DailyValue
    {
        public string Label { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
    }
    public class Ingredient
    {
        public string Text { get; set; }
        public List<ParsedIngredient> Parsed { get; set; }
    }
    public class ParsedIngredient
    {
        public double Quantity { get; set; }
        public string Measure { get; set; }
        public string FoodMatch { get; set; }
        public string Food { get; set; }
        public string FoodId { get; set; }
        public double Weight { get; set; }
        public double RetainedWeight { get; set; }
        public Dictionary<string, Nutrient> Nutrients { get; set; }
        public string MeasureURI { get; set; }
        public string Status { get; set; }
    }
    public class NutrientKCal
    {
        public string Label { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
    }
}

