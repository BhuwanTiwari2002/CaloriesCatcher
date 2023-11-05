namespace CaloriesCatcher.UI.Model.Edamam;

public class RecipeModelEdamam
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public string Uri { get; set; }
    public string Label { get; set; }
    public string Image { get; set; }
    public Dictionary<string, ImageDetail> Images { get; set; }
    public List<string> IngredientLines { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public double Calories { get; set; }
    public double TotalCO2Emissions { get; set; }
    public string CO2EmissionsClass { get; set; }
    public double TotalWeight { get; set; }
    public double TotalTime { get; set; }
    public List<string> CuisineType { get; set; }
    public List<string> MealType { get; set; }
    public List<string> DishType { get; set; }
    public Dictionary<string, Nutrient> TotalNutrients { get; set; }
}

public class ImageDetail
{
    public string Url { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}

public class Ingredient
{
    public string Text { get; set; }
    public double Quantity { get; set; }
    public string Measure { get; set; }
    public string Food { get; set; }
    public double Weight { get; set; }
    public string FoodCategory { get; set; }
    public string FoodId { get; set; }
    public string Image { get; set; }
}

public class Nutrient
{
    public string Label { get; set; }
    public double Quantity { get; set; }
    public string Unit { get; set; }
}