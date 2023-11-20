using Newtonsoft.Json;

namespace CaloriesCatcher.UI.Model.Edamam;

public class NutritionDetail
{
    [JsonProperty("title")]
    public string title { get; set; }
    [JsonProperty("ingr")]
    public List<string> ingr { get; set; }
}